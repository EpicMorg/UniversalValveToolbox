using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using UniversalValveToolbox.Utils.Dto;
using Newtonsoft.Json;

namespace UniversalValveToolbox.Utils {
    class DataManager {
        private readonly string SettingsPath = Path.Combine(Application.StartupPath, "json", "settings.json");

        public Settings ReadSettings() => this.ReadModel<Settings>(SettingsPath);
        public void SaveSettings(Settings settingsDto) => this.WriteModel(SettingsPath, settingsDto);


        private T ReadModel<T>(string path) => JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        private void WriteModel<T>(string path, T value) => File.WriteAllText(path, JsonConvert.SerializeObject(value, Formatting.Indented));
    }
}

namespace UniversalValveToolbox.Utils.Dto {
    public class Addon : BaseModel {
        private int[] engine;

        private string args;

        private string bin;

        private string name;

        public int[] Engine {
            get => this.engine;
            set => this.UpdateField(value, ref this.engine);
        }

        public string Name {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        private string category;

        public string Category {
            get => this.category;
            set => this.UpdateField(value, ref this.category);
        }

        public string Bin {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        public string Args {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }
    }

    public class Engine : BaseModel {
        private int appid;

        public int Appid {
            get => this.appid;
            set => this.UpdateField(value, ref this.appid);
        }

        private string name;

        public string Name {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        private string bin;

        public string Bin {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        private Tool[] tools;

        public Tool[] Tools {
            get => this.tools;
            set => this.UpdateField(value, ref this.tools);
        }
    }

    public class Tool : BaseModel {
        private string args;

        public string Args {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }

        private string bin;

        public string Bin {
            get => this.bin;
            set => this.UpdateField(value, ref this.bin);
        }

        private string name;

        public string Name {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }
    }

    public class VProject : BaseModel {
        private int engine;

        public int Engine {
            get => this.engine;
            set => this.UpdateField(value, ref this.engine);
        }

        private string path;

        public string Path {
            get => this.path;
            set => this.UpdateField(value, ref this.path);
        }

        private string name;

        public string Name {
            get => this.name;
            set => this.UpdateField(value, ref this.name);
        }

        private string args;

        public string Args {
            get => this.args;
            set => this.UpdateField(value, ref this.args);
        }
    }

    public class Project : Dictionary<string, VProject> { }

    public class SettingsViewModel : BaseModel {
        private readonly Settings settings;
        private readonly LanguageProvider languageProvider;

        private int selectedLanguage;

        public int SelectedLanguageIndex {
            get => this.selectedLanguage;
            set {
                if (this.UpdateField(value, ref selectedLanguage)) {
                    this.settings.Language = this.SelectedLanguage;
                }
            }
        }

        public string[] Languages => this.languageProvider.Languages;
        private string SelectedLanguage => this.Languages[this.SelectedLanguageIndex];

        public SettingsViewModel(Settings settings, LanguageProvider languageProvider) {
            this.settings = settings;
            this.languageProvider = languageProvider;

            this.SelectedLanguageIndex = Math.Max(0, Array.IndexOf(this.Languages, settings.Language));
        }
    }

    public class LanguageProvider {
        public string[] Languages { get; } = { "English", "Россиян" };
    }

    public class Settings : BaseModel {
        private string defaultProject;
        private int[] avalibleEnginies;
        private string language;
        private string theme;

        public string DefaultProject {
            get => this.defaultProject;
            set => this.UpdateField(value, ref this.defaultProject);
        }

        public int[] AvalibleEnginies {
            get => this.avalibleEnginies;
            set => this.UpdateField(value, ref this.avalibleEnginies);
        }

        public string Language {
            get => this.language;
            set => this.UpdateField(value, ref this.language);
        }

        public string Theme {
            get => this.theme;
            set => this.UpdateField(value, ref this.theme);
        }
    }

    public abstract class BaseModel : INotifyPropertyChanged {

        protected bool UpdateField<T>(T value, ref T field, [CallerMemberName]string name = null) {
            var updated = !EqualityComparer<T>.Default.Equals(value, field);
            if (updated) {
                field = value;
                OnPropertyChanged(name);
            }
            return updated;
        }

        protected void OnPropertyChanged([CallerMemberName]string name = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
