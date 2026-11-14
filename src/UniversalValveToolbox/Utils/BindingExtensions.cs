using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace UniversalValveToolbox.Utils {
    /// <summary>
    /// Two-way binding of a control property to a (possibly nested) view-model property:
    /// <c>textBoxName.Bind(c => c.Text, model, m => m.SelectProject.Name)</c>.
    /// Control -> model on the control's "&lt;Property&gt;Changed" event; model -> control on PropertyChanged of the
    /// root model and of every INotifyPropertyChanged object along the path (so switching SelectProject refreshes the text).
    /// Replaces the kasthack.binding.wf package (net4x only).
    /// </summary>
    internal static class BindingExtensions {
        public static void Bind<TControl, TValue, TModel>(
            this TControl control,
            Expression<Func<TControl, TValue>> controlProperty,
            TModel model,
            Expression<Func<TModel, TValue>> modelPath)
            where TControl : Control
            where TModel : class, INotifyPropertyChanged {
            var controlProp = ToPath(controlProperty.Body);
            if (controlProp.Count != 1)
                throw new ArgumentException("Bind to a direct property of the control", nameof(controlProperty));
            new Binder<TValue>(control, controlProp[0], model, ToPath(modelPath.Body));
        }

        private static List<PropertyInfo> ToPath(Expression body) {
            var path = new List<PropertyInfo>();
            while (body is MemberExpression member && member.Member is PropertyInfo prop) {
                path.Insert(0, prop);
                body = member.Expression;
            }
            if (!(body is ParameterExpression))
                throw new ArgumentException($"Only property chains are supported: {body}");
            return path;
        }

        private sealed class Binder<TValue> {
            private readonly Control control;
            private readonly PropertyInfo controlProp;
            private readonly object root;
            private readonly List<PropertyInfo> path;
            private readonly List<INotifyPropertyChanged> hooked = new List<INotifyPropertyChanged>();
            private bool updating;

            public Binder(Control control, PropertyInfo controlProp, object root, List<PropertyInfo> path) {
                this.control = control;
                this.controlProp = controlProp;
                this.root = root;
                this.path = path;

                var changed = control.GetType().GetEvent(controlProp.Name + "Changed");
                if (changed != null)
                    changed.AddEventHandler(control, new EventHandler(OnControlChanged));
                control.Disposed += (s, e) => Unhook();

                Rehook();
                PushToControl();
            }

            // object that owns the last property of the path, or null when something on the way is null
            private object Owner() {
                var current = root;
                for (var i = 0; i < path.Count - 1 && current != null; i++)
                    current = path[i].GetValue(current);
                return current;
            }

            private void Rehook() {
                Unhook();
                var current = root;
                for (var i = 0; i < path.Count && current != null; i++) {
                    if (current is INotifyPropertyChanged npc) {
                        npc.PropertyChanged += OnModelChanged;
                        hooked.Add(npc);
                    }
                    if (i < path.Count - 1)
                        current = path[i].GetValue(current);
                }
            }

            private void Unhook() {
                foreach (var npc in hooked)
                    npc.PropertyChanged -= OnModelChanged;
                hooked.Clear();
            }

            private void OnModelChanged(object sender, PropertyChangedEventArgs e) {
                if (updating)
                    return;
                Rehook(); // an object on the path may have been replaced
                PushToControl();
            }

            private void OnControlChanged(object sender, EventArgs e) {
                if (updating)
                    return;
                var owner = Owner();
                var target = path[path.Count - 1];
                if (owner == null || !target.CanWrite)
                    return;

                updating = true;
                try {
                    var value = controlProp.GetValue(control);
                    if (!Equals(target.GetValue(owner), value))
                        target.SetValue(owner, value);
                }
                finally {
                    updating = false;
                }
                Rehook();
            }

            private void PushToControl() {
                var owner = Owner();
                object value = owner == null ? default(TValue) : path[path.Count - 1].GetValue(owner);
                if (value == null && controlProp.PropertyType == typeof(string))
                    value = string.Empty;

                if (Equals(controlProp.GetValue(control), value))
                    return;

                updating = true;
                try {
                    controlProp.SetValue(control, value);
                }
                catch (TargetInvocationException ex) when (ex.InnerException is ArgumentOutOfRangeException) {
                    // e.g. SelectedIndex before the combo box items are filled; the next model change sets it again
                }
                finally {
                    updating = false;
                }
            }
        }
    }
}
