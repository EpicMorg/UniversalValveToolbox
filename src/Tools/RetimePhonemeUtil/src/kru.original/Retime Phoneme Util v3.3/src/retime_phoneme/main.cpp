#include <iostream>
#include <windows.h>
#include <ksmedia.h>
#include <time.h>
#include <shlobj.h>
#include <shlwapi.h>



#define _in_
#define _out_
#define _in_out_

#define _in_opt_
#define _out_opt_
#define _in_out_opt_



//////////////////////////////////////////////////////////////////////////////////////////////// вспомогательные функции
bool FileExist( _in_ const wchar_t * FileName ) {
    DWORD FileAttributes = GetFileAttributesW(FileName);
    return ( (FileAttributes != INVALID_FILE_ATTRIBUTES) &&
             ( ! (FileAttributes & FILE_ATTRIBUTE_DIRECTORY)) );
}



BOOL DirExists( _in_ const wchar_t * DirName )
{
    DWORD FileAttributes = GetFileAttributesW(DirName);
    return ( (FileAttributes != INVALID_FILE_ATTRIBUTES) &&
             (FileAttributes & FILE_ATTRIBUTE_DIRECTORY) );
}



#define PATH_SEPARATOR_TYPE_1 L'\\'
#define PATH_SEPARATOR_TYPE_2 L'/'

int PathCreatePath( _in_ const wchar_t * Path ) { // ex: C:\\Dir1\\Dir2\\Dir3\\...
    bool res = false;

    wchar_t Dir[MAX_PATH];
    register size_t x;
    for (x = 0; Path[x] != '\0'; x++) {
        if ((Path[x] == PATH_SEPARATOR_TYPE_1) ||
            (Path[x] == PATH_SEPARATOR_TYPE_2)) {
            Dir[x] = L'\0';
            if ( ! DirExists(&Dir[0])) {
                if ( ! CreateDirectoryW(&Dir[0], NULL)) {
                    goto finish;
                }
            }
        }
        Dir[x] = Path[x];
    }
    Dir[x] = '\0';
    if ( ! DirExists(&Dir[0])) { // на случай если после последней папки в пути нет слеша
        if ( ! CreateDirectoryW(&Dir[0], NULL)) {
            goto finish;
        }
    }
    res = true;

finish:
    return res;
}



void PathRemoveSlash( _in_out_ wchar_t * Path ) {
    size_t Len = wcslen(Path);
    if (Len > 0) {
        if ((Path[Len-1] == PATH_SEPARATOR_TYPE_1) ||
            (Path[Len-1] == PATH_SEPARATOR_TYPE_2)) {
            Path[Len-1] = L'\0';
        }
    }
    return;
}



void PathExtractFilePath( _in_ const wchar_t * Path, _out_ wchar_t * FilePath ) {
    wchar_t * Slash;
    wcscpy(FilePath, Path);
    PathRemoveSlash(FilePath);
    Slash = wcsrchr(FilePath, PATH_SEPARATOR_TYPE_1);
    if (Slash != NULL ) { *Slash = '\0'; }
    else {
        Slash = wcsrchr(FilePath, PATH_SEPARATOR_TYPE_2);
        if (Slash != NULL ) { *Slash = '\0'; }
    }
    return;
}



long GetFileSize(FILE * file) {
    long current_pos = ftell(file);
    fseek(file, 0LL, SEEK_END);
    long file_size = ftell(file);
    fseek(file, current_pos, SEEK_SET);
    return file_size;
}


// поиск строки в бинарном файле
long fstr( _in_ FILE * file, _in_ const char * str, _in_ const bool case_sensetive = false) {
    typedef int (* __strncmp_fn_t)(const char *, const char *, size_t);
    __strncmp_fn_t __strncmp_fn = (case_sensetive) ? strncmp : strnicmp;

    long res = EOF;

    size_t len = strlen(str);
    char buff[len];
    for (;;) {
        int c = fgetc(file);
        if (c == EOF) { break; }
        if (c == *str) {
            long tmp_pos = ftell(file);
            fseek(file, -1, SEEK_CUR);
            if (fread(&buff[0], len, 1, file)) {
                if (__strncmp_fn(&buff[0], str, len) == 0) {
                    res = tmp_pos - 1;
                } else {
                    fseek(file, tmp_pos, SEEK_SET);
                }
            } else {
                break;
            }
        }
    }

    return res;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



////////////////////////////////////////////////////////////////////////////////////////////////////////////// Wave File
enum WAVEFILETYPE {
    WF_EX  = 1,
    WF_EXT = 2
};

typedef struct {
    char		RIFF[4];
    uint32_t 	RIFFSize;
    char	 	WAVE[4];
} WAVEFILEHEADER;

typedef struct
{
    char		chunk_name[4];
    uint32_t	size;
} RIFFCHUNK;

typedef struct {
    uint16_t	FormatTag;
    uint16_t	Channels;
    uint32_t	SamplesPerSec;
    uint32_t	AvgBytesPerSec;
    uint16_t	BlockAlign;
    uint16_t	BitsPerSample;
    uint16_t	Size;
    uint16_t  	Reserved;
    uint32_t	ChannelMask;
    GUID  		guidSubFormat;
} WAVEFMT;


#ifndef _WAVEFORMATEX_
    #define _WAVEFORMATEX_
typedef struct tWAVEFORMATEX {
    uint16_t    wFormatTag;
    uint16_t    nChannels;
    uint32_t    nSamplesPerSec;
    uint32_t    nAvgBytesPerSec;
    uint16_t    nBlockAlign;
    uint16_t    wBitsPerSample;
    uint16_t    cbSize;
} WAVEFORMATEX;
#endif /* _WAVEFORMATEX_ */

#ifndef _WAVEFORMATEXTENSIBLE_
    #define _WAVEFORMATEXTENSIBLE_
typedef struct {
    WAVEFORMATEX    Format;
    union {
        uint16_t    ValidBitsPerSample;   /* bits of precision  */
		uint16_t    SamplesPerBlock;      /* valid if BitsPerSample==0 */
		uint16_t    Reserved;             /* If neither applies, set to zero. */
    } Samples;
    uint32_t        ChannelMask;          /* which channels are present in stream  */
    GUID            SubFormat;
} WAVEFORMATEXTENSIBLE, *PWAVEFORMATEXTENSIBLE;
#endif // _WAVEFORMATEXTENSIBLE_

typedef struct
{
    WAVEFILETYPE	        wfType;
    WAVEFORMATEXTENSIBLE    wfEXT;		// For non-WAVEFORMATEXTENSIBLE wavefiles, the header is stored in the Format member of wfEXT
    FILE *                  file;
    long	                data_size;
    long	                data_offset;
} WAVEFILEINFO, * LPWAVEFILEINFO;

bool OpenWaveFile( _in_ const wchar_t * FileName, _out_ WAVEFILEINFO * WaveInfo) {
    WAVEFILEHEADER	waveFileHeader;
    RIFFCHUNK		riffChunk;
    WAVEFMT			waveFmt;
    bool		    res = false;

    memset(WaveInfo, 0, sizeof(WAVEFILEINFO));

    // Open the wave file for reading
    WaveInfo->file = _wfopen(FileName, L"rb");
    if (WaveInfo->file != NULL) {
        // Read Wave file header
        fread(&waveFileHeader, 1, sizeof(WAVEFILEHEADER), WaveInfo->file);
        if ((strnicmp(waveFileHeader.RIFF, "RIFF", 4) == 0) && (strnicmp(waveFileHeader.WAVE, "WAVE", 4) == 0)) {
            while (fread(&riffChunk, 1, sizeof(RIFFCHUNK), WaveInfo->file) == sizeof(RIFFCHUNK)) {
                if (strnicmp(riffChunk.chunk_name, "fmt ", 4) == 0) {
                    if (riffChunk.size <= sizeof(WAVEFMT)) {
                        fread(&waveFmt, 1, riffChunk.size, WaveInfo->file);
                        // Determine if this is a WAVEFORMATEX or WAVEFORMATEXTENSIBLE wave file
                        if (waveFmt.FormatTag == WAVE_FORMAT_PCM) {
                            WaveInfo->wfType = WF_EX;
                            memcpy(&WaveInfo->wfEXT.Format, &waveFmt, sizeof(PCMWAVEFORMAT));
                        } else if (waveFmt.FormatTag == WAVE_FORMAT_EXTENSIBLE) {
                            WaveInfo->wfType = WF_EXT;
                            memcpy(&WaveInfo->wfEXT, &waveFmt, sizeof(WAVEFORMATEXTENSIBLE));
                        }
                    } else {
                        fseek(WaveInfo->file, riffChunk.size, SEEK_CUR);
                    }
                } else if (strnicmp(riffChunk.chunk_name, "data", 4) == 0) {
                    WaveInfo->data_size = riffChunk.size;
                    WaveInfo->data_offset = ftell(WaveInfo->file);
                    fseek(WaveInfo->file, riffChunk.size, SEEK_CUR);
                } else {
                    fseek(WaveInfo->file, riffChunk.size, SEEK_CUR);
                }
                // Ensure that we are correctly aligned for next chunk
                if (riffChunk.size & 1) {
                    fseek(WaveInfo->file, 1, SEEK_CUR);
                }
            }
            if (WaveInfo->data_size &&
                WaveInfo->data_offset &&
                ((WaveInfo->wfType == WF_EX) || (WaveInfo->wfType == WF_EXT)))
            {
                res = true;
            } else {
                fclose(WaveInfo->file);
            }
        }
    }

    return res;
}



bool CopyWaveFile(const WAVEFILEINFO * WaveInfo,
                  const wchar_t * DstWaveFileName,
                  const bool WithoutDataOnEnd)
{
    if ( ! FileExist(DstWaveFileName)) {
        wchar_t DstWaveFilePath[MAX_PATH];
        PathExtractFilePath(DstWaveFileName, DstWaveFilePath);
        if ( ! DirExists(DstWaveFilePath)) {
            if ( ! PathCreatePath(DstWaveFilePath)) {
                return false;
            }
        }
    }

#define READ_WRITE_BUFF_SIZE 65536
    bool res = false;

    FILE * DstFile = _wfopen(DstWaveFileName, L"wb");
    if (DstFile != NULL) {

        long CopySize = (WithoutDataOnEnd) ?
                        WaveInfo->data_offset + WaveInfo->data_size :
                        GetFileSize(WaveInfo->file);

        uint8_t ReadWriteBuff[READ_WRITE_BUFF_SIZE];
        size_t ReadWriteSize = READ_WRITE_BUFF_SIZE;

        fseek(WaveInfo->file, 0LL, SEEK_SET);
        do {
            if (CopySize < READ_WRITE_BUFF_SIZE) {
                if (CopySize == 0) { break; }
                ReadWriteSize = (size_t) CopySize;
            }

            fread(&ReadWriteBuff[0], ReadWriteSize, 1, WaveInfo->file);
            fwrite(&ReadWriteBuff[0], ReadWriteSize, 1, DstFile);

            CopySize -= ReadWriteSize;
        } while (CopySize > 0);

        fclose(DstFile);
        res = true;
    }

    return res;
}



double GetWaveFileDuration( _in_ WAVEFILEINFO * WaveInfo ) {
#define BITS_IN_BYTE 8
    int64_t Samples = ((WaveInfo->data_size * BITS_IN_BYTE) / WaveInfo->wfEXT.Format.wBitsPerSample) / WaveInfo->wfEXT.Format.nChannels;
    return ((double)Samples / (double)WaveInfo->wfEXT.Format.nSamplesPerSec);
}



void CloseWaveFile( _in_out_ WAVEFILEINFO * WaveInfo) {
    if ((WaveInfo != NULL) && (WaveInfo->file != NULL)) {
        fclose(WaveInfo->file);
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////////////////////////////////////  Logging
#define UNDEF_TEXT_LEN -1

FILE * LogFile = NULL;

static char time_buff[32];
char * GetFmtTimeStr() {
    SYSTEMTIME stUTC, stLocal;
    GetSystemTime(&stUTC);
    if (SystemTimeToTzSpecificLocalTime(NULL, &stUTC, &stLocal)) {
        sprintf(time_buff, "%02d.%02d.%04d %02d:%02d:%02d.%03d",
                stLocal.wMonth, stLocal.wDay, stLocal.wYear,
                stLocal.wHour, stLocal.wMinute, stLocal.wSecond, stLocal.wMilliseconds);
    } else {
        sprintf(time_buff, "%02d.%02d.%04d %02d:%02d:%02d.%03d",
                stUTC.wMonth, stUTC.wDay, stUTC.wYear,
                stUTC.wHour, stUTC.wMinute, stUTC.wSecond, stUTC.wMilliseconds);
    }
    return time_buff;
}



void CreateLog() {
#define LOG_FILE_NAME L"Log.rtf"
    LogFile = _wfopen(LOG_FILE_NAME, FileExist(LOG_FILE_NAME) ? L"ab" : L"wb");
}



void SaveAsciiTextToLog( _in_ const char * AsciiText, _in_opt_ int AsciiTextLen = UNDEF_TEXT_LEN ) {
    if (AsciiTextLen == UNDEF_TEXT_LEN) {
        AsciiTextLen = (int) strlen(AsciiText);
    }
    if (LogFile != NULL) {
        fwrite(AsciiText, (size_t) AsciiTextLen, 1, LogFile);
    }
}



void SaveWideTextToLog( _in_ const wchar_t * WideText, _in_opt_ int WideTextLen = UNDEF_TEXT_LEN ) {
    if (WideTextLen == UNDEF_TEXT_LEN) {
        WideTextLen = (int) wcslen(WideText);
    }
    int AsciiTextLen = WideCharToMultiByte(CP_UTF8, 0, WideText, WideTextLen, NULL, 0, NULL, NULL);
    char AsciiText[AsciiTextLen];
    if (WideCharToMultiByte(CP_UTF8, 0, WideText, WideTextLen, AsciiText, AsciiTextLen, NULL, NULL) != 0) {
        if (LogFile != NULL) {
            fwrite(&AsciiText[0], (size_t) AsciiTextLen, 1, LogFile);
        }
    }
}



void SaveDoubleToLog( _in_ const double Double ) {
    char buff[16];
    sprintf(buff, "%.3f", Double);
    SaveAsciiTextToLog(buff);
}



void CloseLog() {
    if (LogFile != NULL) {
        fclose(LogFile);
        LogFile = NULL;
    }
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



//////////////////////////////////////////////////////////////////////////////////////////////////////////// Raw Phoneme
typedef struct {
    char		VDAT[4]; // "VDAT"
    uint32_t 	size; // длина текста
} VDATCHUNK;

typedef struct {
    VDATCHUNK   vdat_chunk;
    char *      text;
} RAW_PHONEME;



bool LoadRawPhoneme( _in_ const WAVEFILEINFO * WaveInfo, _out_ RAW_PHONEME * raw_phoneme) {
    bool res = false;
    memset(raw_phoneme, 0, sizeof(RAW_PHONEME));

    long FileSize = GetFileSize(WaveInfo->file);
    long WaveSize = WaveInfo->data_offset + WaveInfo->data_size;
    long FileRestSize = FileSize - WaveSize;
    if (FileRestSize > 0) {
        fseek(WaveInfo->file, WaveSize, SEEK_SET);
        long vdat_chank_offset = fstr(WaveInfo->file, "VDAT");
        if (vdat_chank_offset != EOF) {
            fseek(WaveInfo->file, vdat_chank_offset, SEEK_SET);
            fread(&raw_phoneme->vdat_chunk, sizeof(VDATCHUNK), 1, WaveInfo->file);
            raw_phoneme->text = (char *) malloc(raw_phoneme->vdat_chunk.size + 1); // +1 для '\0'. проще для лога и дебага, когда есль ноль на конце
            if (raw_phoneme->text != NULL) {
                if (fread(raw_phoneme->text, raw_phoneme->vdat_chunk.size, 1, WaveInfo->file)) {
                    raw_phoneme->text[raw_phoneme->vdat_chunk.size] = '\0';
                    res = true;
                }
            }
        }
    }

    return res;
}



bool SaveRawPhoneme( _in_ const RAW_PHONEME * raw_phoneme, _in_ const wchar_t * FileName) {
    bool res = false;

    FILE * f = _wfopen(FileName, L"ab");
    if (f != NULL) {
        if (fwrite(&raw_phoneme->vdat_chunk, sizeof(VDATCHUNK), 1, f)) {
            if (fwrite(raw_phoneme->text, raw_phoneme->vdat_chunk.size, 1, f)) {
                res = true;
            }
        }
        fclose(f);
    }

    return res;
}



void UnloadRawPhoneme( _in_out_ RAW_PHONEME * raw_phoneme ) {
    if ((raw_phoneme != NULL) && (raw_phoneme->text != NULL)) {
        free(raw_phoneme->text);
    }
}



//////////////////////////////////////////////////////////////////////////////////////////////////////////////// Phoneme
typedef struct {
    uint16_t     id;
    char         name[8];
    double       start_time;
    double       end_time;
    char         _1;
} PHONEME_ENTRY;

typedef struct {
    char *       text;
    double       start_time;
    double       end_time;

    uint32_t     num_phonemes;
    PHONEME_ENTRY * phoneme;
} WORD_ENTRY;

typedef struct {
    double       time;
    double       normalised_value;
} EMPHASIS_ENTRY;



typedef struct {
    VDATCHUNK vdat_chunk;

    char * version;

    struct {
        char * text;
    } PLAINTEXT;

    struct {
        uint32_t num_words;
        WORD_ENTRY * word;
    } WORDS;

    struct {
        uint32_t     num_emphasis;
        EMPHASIS_ENTRY * emphasis;
    } EMPHASIS;

    // struct {
    //
    // } CLOSECAPTION; // не используется

    struct {
        char voice_duck;
    } OPTIONS;

} PHONEME;



typedef enum {
    PHONEME_OK = 0,

    PHONEME_UNEXPECTED_END  = 1,

    PHONEME_MEMORY_ALLOCATION_ERROR = 3,
    PHONEME_MEMORY_REALLOCATION_ERROR,

    PHONEME_HAS_NO_PLAINTEXT_SECTION,
    PHONEME_PLAINTEXT_SECTION_IS_EMPTY, // хотя может это и не ошибка, но пока сделаю ошибкой

    PHONEME_HAS_NO_WORDS_SECTION,
    PHONEME_WORDS_SECTION_HAS_NO_WORDS,

    PHONEME_WORD_VALUE_IS_LOST,
    PHONEME_WORD_HAS_NO_PHONEMES,
    PHONEME_WORD_PHONEME_VALUE_IS_LOST,

    PHONEME_HAS_NO_EMPHASIS_SECTION,
    PHONEME_EMPHASIS_VALUE_IS_LOST,

    PHONEME_HAS_NO_OPTIONS_SECTION,
    PHONEME_OPTIONS_VALUE_IS_LOST,

} PHONEME_RESULT;



const char * GetPhonemeResultStr( _in_ const PHONEME_RESULT res ) {
    switch (res) {
        case PHONEME_OK: return "OK"; break;
        case PHONEME_UNEXPECTED_END: return "ERROR: unexpected end of the phoneme"; break;
        case PHONEME_MEMORY_ALLOCATION_ERROR: return "ERROR: memory allocation error"; break;
        case PHONEME_MEMORY_REALLOCATION_ERROR: return "ERROR: memory reallocation error"; break;
        case PHONEME_HAS_NO_PLAINTEXT_SECTION: return "ERROR: phoneme has no \"PLAINTEXT\" section"; break;
        case PHONEME_PLAINTEXT_SECTION_IS_EMPTY: return "ERROR: phoneme \"PLAINTEXT\" section is empty"; break;
        case PHONEME_HAS_NO_WORDS_SECTION: return "ERROR: phoneme has no \"WORDS\" section"; break;
        case PHONEME_WORDS_SECTION_HAS_NO_WORDS: return "ERROR: phoneme \"WORDS\" section has no \"WORD\"s"; break;
        case PHONEME_WORD_VALUE_IS_LOST: return "ERROR: phoneme \"WORD\" value not found"; break;
        case PHONEME_WORD_HAS_NO_PHONEMES: return "ERROR: phoneme \"WORD\" has no phonemes"; break;
        case PHONEME_WORD_PHONEME_VALUE_IS_LOST: return "ERROR: phoneme \"WORD\" phoneme value not found"; break;
        case PHONEME_HAS_NO_EMPHASIS_SECTION: return "ERROR: phoneme has no \"EMPHASIS\" section"; break;
        case PHONEME_EMPHASIS_VALUE_IS_LOST: return "ERROR: phoneme \"EMPHASIS\" section value not found"; break;
        case PHONEME_HAS_NO_OPTIONS_SECTION: return "ERROR: phoneme has no \"OPTIONS\" section"; break;
        case PHONEME_OPTIONS_VALUE_IS_LOST: return "ERROR: phoneme \"OPTIONS\" section value not found"; break;
    }
}



PHONEME_RESULT RawPhonemeToPhoneme( _in_ const RAW_PHONEME * raw_phoneme, _out_ PHONEME * phoneme ) {
    PHONEME_RESULT res = PHONEME_OK;

    memset(phoneme, 0, sizeof(PHONEME));

    char * raw_text = raw_phoneme->text;
    char * end_of_line;

#define ___move_to_next_line \
    end_of_line = strchr(raw_text, '\n'); \
    if (end_of_line != NULL) { \
        raw_text = end_of_line + 1; \
    } else { \
        res = PHONEME_UNEXPECTED_END; \
        goto finish; \
    } \


    // VERSION 1.0
    end_of_line = strchr(raw_text, '\n');
    if (end_of_line != NULL) {
        const size_t version_str_len = (end_of_line - raw_text);
        phoneme->version = (char *) malloc(version_str_len + 1); // +1 для '\0'
        if (phoneme->version != NULL) {
            strncpy(phoneme->version, raw_text, version_str_len);
            phoneme->version[version_str_len] = '\0';
            ___move_to_next_line;
        } else  {
            res = PHONEME_MEMORY_ALLOCATION_ERROR;
            goto finish;
        }
    } else {
        res = PHONEME_UNEXPECTED_END;
        goto finish;
    }


    // PLAINTEXT
#define PLAINTEXT_STR "PLAINTEXT"
#define PLAINTEXT_STR_LEN strlen(PLAINTEXT_STR)
    if (strnicmp(raw_text, PLAINTEXT_STR, PLAINTEXT_STR_LEN) == 0) {
        raw_text += PLAINTEXT_STR_LEN; //
        ___move_to_next_line;
        ___move_to_next_line;
        end_of_line = strchr(raw_text, '\n'); // ищем конец строки текста
        if (end_of_line != NULL) {
            const size_t plaintext_str_len = (end_of_line - raw_text);
            if (plaintext_str_len > 0) {
                phoneme->PLAINTEXT.text = (char *) malloc(plaintext_str_len + 1); // +1 для '\0'
                if (phoneme->PLAINTEXT.text != NULL) {
                    strncpy(phoneme->PLAINTEXT.text, raw_text, plaintext_str_len);
                    phoneme->PLAINTEXT.text[plaintext_str_len] = '\0';
                    ___move_to_next_line;
                    ___move_to_next_line;
                } else {
                    res = PHONEME_MEMORY_ALLOCATION_ERROR;
                    goto finish;
                }
            } else {
                res = PHONEME_PLAINTEXT_SECTION_IS_EMPTY;
                goto finish;
            }
        } else {
            res = PHONEME_UNEXPECTED_END;
            goto finish;
        }
    } else {
        res = PHONEME_HAS_NO_PLAINTEXT_SECTION;
        goto finish;
    }

#define MAX_TIME_STR_LEN 16
    char * space;
    size_t len;
    char time_str[MAX_TIME_STR_LEN];

    // WORDS
#define WORDS_STR "WORDS"
#define WORDS_STR_LEN strlen(WORDS_STR)
    if (strnicmp(raw_text, WORDS_STR, WORDS_STR_LEN) == 0) {
        ___move_to_next_line; // текущая строка
        ___move_to_next_line; // '{'

        // WORD
    #define WORD_STR "WORD "
    #define WORD_STR_LEN strlen(WORD_STR)
        if (strnicmp(raw_text, WORD_STR, WORD_STR_LEN) == 0) { // найдено первый WORD
            do {// парсим WORD
                raw_text += WORD_STR_LEN; // пропуск текста "WORD "
                phoneme->WORDS.num_words++;
                phoneme->WORDS.word = (WORD_ENTRY *) realloc(phoneme->WORDS.word,
                                                             phoneme->WORDS.num_words * sizeof(WORD_ENTRY));
                if (phoneme->WORDS.word != NULL) {
                    const size_t w = phoneme->WORDS.num_words - 1; // индекс текущего WORD
                    memset(&phoneme->WORDS.word[w], 0, sizeof(WORD_ENTRY));

                    // 1. слово
                    space = strchr(raw_text, ' ');
                    if (space != NULL) {
                        len = space - raw_text;
                        if (len > 0) {
                            phoneme->WORDS.word[w].text = (char *) malloc(len + 1); // + 1 для '\0'
                            if (phoneme->WORDS.word[w].text != NULL) {
                                strncpy(phoneme->WORDS.word[w].text, raw_text, len);
                                phoneme->WORDS.word[w].text[len] = '\0';
                                raw_text = space + 1; // + 1 для ' '
                            } else {
                                res = PHONEME_MEMORY_ALLOCATION_ERROR;
                                goto finish;
                            }
                        } else {
                            res = PHONEME_WORD_VALUE_IS_LOST;
                            goto finish;
                        }
                    } else {
                        res = PHONEME_UNEXPECTED_END;
                        goto finish;
                    }

                    // 2. <start time>
                    space = strchr(raw_text, ' ');
                    if (space != NULL) {
                        len = space - raw_text;
                        if (len > 0) {
                            strncpy(time_str, raw_text, len);
                            time_str[len] = '\0';
                            phoneme->WORDS.word[w].start_time = atof(time_str);
                            raw_text = space + 1; // + 1 для ' '
                        } else {
                            res = PHONEME_WORD_VALUE_IS_LOST;
                            goto finish;
                        }
                    } else {
                        res = PHONEME_UNEXPECTED_END;
                        goto finish;
                    }
                    // 3. <end time>
                    end_of_line = strchr(raw_text, '\n');
                    if (end_of_line != NULL) {
                        len = end_of_line - raw_text;
                        if (len > 0) {
                            strncpy(time_str, raw_text, len);
                            time_str[len] = '\0';
                            phoneme->WORDS.word[w].end_time = atof(time_str);
                            ___move_to_next_line; // на след. строку
                            ___move_to_next_line; // пропуск '{'
                        } else {
                            res = PHONEME_WORD_VALUE_IS_LOST;
                            goto finish;
                        }
                    } else {
                        res = PHONEME_UNEXPECTED_END;
                        goto finish;
                    }

                    // парсим фонемы слова
                    if (isdigit(*raw_text)) { // строка фонемы начинается с числа
                        do {
                            phoneme->WORDS.word[w].num_phonemes++;
                            phoneme->WORDS.word[w].phoneme = (PHONEME_ENTRY *)realloc(phoneme->WORDS.word[w].phoneme,
                                                                                      phoneme->WORDS.word[w].num_phonemes * sizeof(PHONEME_ENTRY));
                            if (phoneme->WORDS.word[w].phoneme != NULL) {
                                const size_t p = phoneme->WORDS.word[w].num_phonemes - 1; // индекс текущей строки фонемы
                                memset(&phoneme->WORDS.word[w].phoneme[p], 0, sizeof(PHONEME_ENTRY));

                                // 1. ID
                                space = strchr(raw_text, ' ');
                                if (space != NULL) {
                                    len = space - raw_text;
                                    if (len > 0) {
                                        strncpy(time_str, raw_text, len);
                                        time_str[len] = '\0';
                                        phoneme->WORDS.word[w].phoneme[p].id = (uint16_t) atoi(time_str);
                                        raw_text = space + 1; // + 1 для ' '
                                    } else {
                                        res = PHONEME_WORD_PHONEME_VALUE_IS_LOST;
                                        goto finish;
                                    }
                                } else {
                                    res = PHONEME_UNEXPECTED_END;
                                    goto finish;
                                }

                                // 2. name
                                space = strchr(raw_text, ' ');
                                if (space != NULL) {
                                    len = space - raw_text;
                                    if (len > 0) {
                                        strncpy(phoneme->WORDS.word[w].phoneme[p].name, raw_text, len);
                                        phoneme->WORDS.word[w].phoneme[p].name[len] = '\0';
                                        raw_text = space + 1; // + 1 для ' '
                                    } else {
                                        res = PHONEME_WORD_PHONEME_VALUE_IS_LOST;
                                        goto finish;
                                    }
                                } else {
                                    res = PHONEME_UNEXPECTED_END;
                                    goto finish;
                                }

                                // 3. <start time>
                                space = strchr(raw_text, ' ');
                                if (space != NULL) {
                                    len = space - raw_text;
                                    if (len > 0) {
                                        strncpy(time_str, raw_text, len);
                                        time_str[len] = '\0';
                                        phoneme->WORDS.word[w].phoneme[p].start_time = atof(time_str);
                                        raw_text = space + 1; // + 1 для ' '
                                    } else {
                                        res = PHONEME_WORD_PHONEME_VALUE_IS_LOST;
                                        goto finish;
                                    }
                                } else {
                                    res = PHONEME_UNEXPECTED_END;
                                    goto finish;
                                }

                                // 4. <end time>
                                space = strchr(raw_text, ' ');
                                if (space != NULL) {
                                    len = space - raw_text;
                                    if (len > 0) {
                                        strncpy(time_str, raw_text, len);
                                        time_str[len] = '\0';
                                        phoneme->WORDS.word[w].phoneme[p].end_time = atof(time_str);
                                        raw_text = space + 1; // + 1 для ' '
                                    } else {
                                        res = PHONEME_WORD_PHONEME_VALUE_IS_LOST;
                                        goto finish;
                                    }
                                } else {
                                    res = PHONEME_UNEXPECTED_END;
                                    goto finish;
                                }

                                // 5. неиспользуемая 1 на конце
                                phoneme->WORDS.word[w].phoneme[p]._1 = *raw_text;
                                ___move_to_next_line;

                            } else {
                                res = PHONEME_MEMORY_REALLOCATION_ERROR;
                                goto finish;
                            }
                        } while (isdigit(*raw_text));
                    } else { //ошибка. фонем у слова нет
                        res = PHONEME_WORD_HAS_NO_PHONEMES;
                        goto finish;
                    }

                    ___move_to_next_line; // пропуск '}' WORD-а

                } else {
                    res = PHONEME_MEMORY_REALLOCATION_ERROR;
                    goto finish;
                }
            } while (strnicmp(raw_text, WORD_STR, WORD_STR_LEN) == 0);
        } else {
            res = PHONEME_WORDS_SECTION_HAS_NO_WORDS;
            goto finish;
        }

        ___move_to_next_line; // пропуск '}' WORDS-а

    } else {
        res = PHONEME_HAS_NO_WORDS_SECTION;
        goto finish;
    }


    // EMPHASIS
#define EMPHASIS_STR "EMPHASIS"
#define EMPHASIS_STR_LEN strlen(EMPHASIS_STR)
    if (strnicmp(raw_text, EMPHASIS_STR, EMPHASIS_STR_LEN) == 0) {
        ___move_to_next_line; // текущая строка
        ___move_to_next_line; // '{'
        if (isdigit(*raw_text)) {  // значения EMPHASIS начинаются с числа
            do {
                phoneme->EMPHASIS.num_emphasis++;
                phoneme->EMPHASIS.emphasis = (EMPHASIS_ENTRY *) realloc(phoneme->EMPHASIS.emphasis,
                                                                        phoneme->EMPHASIS.num_emphasis * sizeof(EMPHASIS_ENTRY));
                if (phoneme->EMPHASIS.emphasis != NULL) {
                    const size_t e = phoneme->EMPHASIS.num_emphasis - 1; // индекс текущего EMPHASIS значения
                    memset(&phoneme->EMPHASIS.emphasis[e], 0, sizeof(EMPHASIS_ENTRY));

                    // 1. <time>
                    space = strchr(raw_text, ' ');
                    if (space != NULL) {
                        len = space - raw_text;
                        if (len > 0) {
                            strncpy(time_str, raw_text, len);
                            time_str[len] = '\0';
                            phoneme->EMPHASIS.emphasis[e].time = atof(time_str);
                            raw_text = space + 1; // + 1 для ' '
                        } else {
                            res = PHONEME_EMPHASIS_VALUE_IS_LOST; // если уж и есть значения, то их должно быть 2 на каждую строку
                            goto finish;
                        }
                    } else {
                        res = PHONEME_UNEXPECTED_END;
                        goto finish;
                    }

                    // 2. <normalised value>
                    end_of_line = strchr(raw_text, '\n');
                    if (end_of_line != NULL) {
                        len = end_of_line - raw_text;
                        if (len > 0) {
                            strncpy(time_str, raw_text, len);
                            time_str[len] = '\0';
                            phoneme->EMPHASIS.emphasis[e].normalised_value = atof(time_str);
                        } else {
                            res = PHONEME_EMPHASIS_VALUE_IS_LOST;
                            goto finish;
                        }
                    } else {
                        res = PHONEME_UNEXPECTED_END;
                        goto finish;
                    }

                    ___move_to_next_line;
                }
            } while (isdigit(*raw_text));
        } else {
            // EMPHASIS не содержит значений и это не ошибка
        }

        ___move_to_next_line;
    } else {
        res = PHONEME_HAS_NO_EMPHASIS_SECTION;
        goto finish;
    }


    // CLOSECAPTION
        // не используется
    //


    // OPTIONS
#define OPTIONS_STR "OPTIONS"
#define OPTIONS_STR_LEN strlen(OPTIONS_STR)
    if (strnicmp(raw_text, OPTIONS_STR, OPTIONS_STR_LEN) == 0) {
        ___move_to_next_line; // текущая строка
        ___move_to_next_line; // '{'
        space = strchr(raw_text, ' ');
        if (space != NULL) {
            len = space - raw_text;
            if (len > 0) {
                raw_text = space + 1; // + 1 для ' '
                phoneme->OPTIONS.voice_duck = *raw_text;
            } else {
                res = PHONEME_OPTIONS_VALUE_IS_LOST;
                goto finish;
            }
        } else {
            res = PHONEME_UNEXPECTED_END;
            goto finish;
        }
    } else {
        res = PHONEME_HAS_NO_OPTIONS_SECTION;
        goto finish;
    }

    // debug
    //printf("'%s'", raw_text);
    //

    // скопировать vdat_chunk из RAW_PHONEME в PHONEME
    memcpy(&phoneme->vdat_chunk, &raw_phoneme->vdat_chunk, sizeof(VDATCHUNK));

    if (res != PHONEME_OK)
    {
finish:
        // EMPHASIS
        if (phoneme->EMPHASIS.emphasis != NULL) {
            free(phoneme->EMPHASIS.emphasis);
        }

        // WORD
        for (register size_t w = 0; w < phoneme->WORDS.num_words; w++) {
            if (phoneme->WORDS.word[w].text != NULL) {
                free(phoneme->WORDS.word[w].text);
            }
            if (phoneme->WORDS.word[w].phoneme != NULL) {
                free(phoneme->WORDS.word[w].phoneme);
            }
        }
        free(phoneme->WORDS.word);

        // PLAINTEXT
        if (phoneme->PLAINTEXT.text != NULL) {
            free(phoneme->PLAINTEXT.text);
        }

        // VERSION 1.0
        if (phoneme->version != NULL) {
            free(phoneme->version);
        }
    }

    return res;
}


void FreePhoneme( _in_out_ PHONEME * phoneme ) {
    // EMPHASIS
    if (phoneme->EMPHASIS.emphasis != NULL) {
        free(phoneme->EMPHASIS.emphasis);
    }

    // WORD
    for (register size_t w = 0; w < phoneme->WORDS.num_words; w++) {
        if (phoneme->WORDS.word[w].text != NULL) {
            free(phoneme->WORDS.word[w].text);
        }
        if (phoneme->WORDS.word[w].phoneme != NULL) {
            free(phoneme->WORDS.word[w].phoneme);
        }
    }
    free(phoneme->WORDS.word);

    // PLAINTEXT
    if (phoneme->PLAINTEXT.text != NULL) {
        free(phoneme->PLAINTEXT.text);
    }

    // VERSION 1.0
    if (phoneme->version != NULL) {
        free(phoneme->version);
    }
}


size_t strcpy_ex( _in_ char * dst, _in_ const char * src, _in_opt_ const char * append ) {
    register size_t r_len = 0;
    register size_t x;

    for (x = 0; src[x] != '\0'; x++, r_len++) { *dst++ = src[x]; }

    if (append != NULL) {
        for (x = 0; append[x] != '\0'; x++, r_len++) { *dst++ = append[x]; }
    }

    return r_len;
}



/*
void ___debug_msg(const int x) {
    char time_str[20];
    sprintf(time_str, "%d", x);
    MessageBoxA(0, time_str, NULL, MB_OK);
}



void ___debug_print(const char * str, size_t len) {
    for (size_t x = 0; x < len; x++) {
        printf("%с", str[x]);
    }
}
*/



// x.123
#define PHONEME_START_TIME_AND_END_TIME_PRECISION 3

// x.123456  хотя может 6 чисел после запятой не фиксированная длина, но пока оставлю так
#define PHONEME_EMPHASIS_TIME_AND_NORMALIZED_VALUE_PRECISION 6

size_t CalculatePhonemeRawTextLen( _in_ const PHONEME * phoneme ) {
    size_t r_len = 0;

#define MAX_TIME_STR_LEN 16
    char time_str[MAX_TIME_STR_LEN];
#define ___increment_r_len_by_time_str_len(time, precision) \
    sprintf(time_str, "%.*f", precision, time); \
    r_len += strlen(time_str); \

#define ___increment_r_len_by_phoneme_id_str_len(id) \
    sprintf(time_str, "%d", id); \
    r_len += strlen(time_str); \


    // VERSION 1.0
    r_len += strlen(phoneme->version) + 1; // + 1 для '\n'
    // PLAINTEXT
#define PLAINTEXT_STR "PLAINTEXT"
#define PLAINTEXT_STR_LEN strlen(PLAINTEXT_STR)
    r_len += PLAINTEXT_STR_LEN + 3; // + 3 для '\n','{','\n'
    r_len += strlen(phoneme->PLAINTEXT.text) + 3; // + 3 для '\n','}','\n'

    // WORDS
#define WORDS_STR "WORDS"
#define WORDS_STR_LEN strlen(WORDS_STR)
    r_len += WORDS_STR_LEN + 3; // + 3 для '\n','{','\n'
    {
        // WORD
        #define WORD_STR "WORD "
        #define WORD_STR_LEN strlen(WORD_STR)
        for (register size_t w = 0; w < phoneme->WORDS.num_words; w++) {
            r_len += WORD_STR_LEN;
            r_len += strlen(phoneme->WORDS.word[w].text) + 1; // + 1 для ' '
            ___increment_r_len_by_time_str_len(phoneme->WORDS.word[w].start_time, PHONEME_START_TIME_AND_END_TIME_PRECISION);
            r_len += 1; // + 1 для ' '
            ___increment_r_len_by_time_str_len(phoneme->WORDS.word[w].end_time, PHONEME_START_TIME_AND_END_TIME_PRECISION);
            r_len += 3; // + 3 для '\n','{','\n'

            // PHONEME
            for (register size_t p = 0; p < phoneme->WORDS.word[w].num_phonemes; p++) {
                ___increment_r_len_by_phoneme_id_str_len(phoneme->WORDS.word[w].phoneme[p].id);
                r_len += 1; // + 1 для ' '
                r_len += strlen(phoneme->WORDS.word[w].phoneme[p].name);
                r_len += 1; // + 1 для ' '
                ___increment_r_len_by_time_str_len(phoneme->WORDS.word[w].phoneme[p].start_time, PHONEME_START_TIME_AND_END_TIME_PRECISION);
                r_len += 1; // + 1 для ' '
                ___increment_r_len_by_time_str_len(phoneme->WORDS.word[w].phoneme[p].end_time, PHONEME_START_TIME_AND_END_TIME_PRECISION);
                r_len += 3; // + 2 для ' ','1','\n'
            }
            r_len += 2; // + 2 для '}','\n'
        }
        r_len += 2; // + 2 для '}','\n'
    }



    // EMPHASIS
#define EMPHASIS_STR "EMPHASIS"
#define EMPHASIS_STR_LEN strlen(EMPHASIS_STR)
    r_len += EMPHASIS_STR_LEN + 3; // + 3 для '\n','{','\n'
    for (register size_t e = 0; e < phoneme->EMPHASIS.num_emphasis; e++) {
        ___increment_r_len_by_time_str_len(phoneme->EMPHASIS.emphasis[e].time, PHONEME_EMPHASIS_TIME_AND_NORMALIZED_VALUE_PRECISION);
        r_len += 1; // + 1 для ' '
        ___increment_r_len_by_time_str_len(phoneme->EMPHASIS.emphasis[e].normalised_value, PHONEME_EMPHASIS_TIME_AND_NORMALIZED_VALUE_PRECISION);
        r_len += 1; // + 1 для '\n'
    }
    r_len += 2; // + 2 для '}','\n'


    // CLOSECAPTION
    // не используется
    //


    // OPTIONS
#define OPTIONS_STR "OPTIONS"
#define OPTIONS_STR_LEN strlen(OPTIONS_STR)
    r_len += OPTIONS_STR_LEN + 3; // + 3 для '\n','{','\n'
    {
    #define voice_duck_str "voice_duck "
    #define voice_duck_str_len strlen(voice_duck_str)
        r_len += voice_duck_str_len;
        r_len += 2; // + 2 для '1 или 0','\n'
    }
    r_len += 2; // + 2 для '}','\n'

    return r_len;
}



PHONEME_RESULT PhonemeToRawPhoneme( _in_ const PHONEME * phoneme, _out_ RAW_PHONEME * raw_phoneme ) {
    memset(raw_phoneme, 0, sizeof(RAW_PHONEME));

    strncpy(raw_phoneme->vdat_chunk.VDAT, "VDAT", 4);
    raw_phoneme->vdat_chunk.size = (uint32_t) CalculatePhonemeRawTextLen(phoneme);
    raw_phoneme->text = (char *) malloc(raw_phoneme->vdat_chunk.size + 1); // + 1 для '\0'
    if (raw_phoneme->text != NULL) {
        char * raw_text = raw_phoneme->text;

    #define MAX_TIME_STR_LEN 16
        char time_str[MAX_TIME_STR_LEN];
    #define ___copy_time_to_raw_text(time, precision, append) \
        sprintf(time_str, "%.*f", precision, time); \
        raw_text += strcpy_ex(raw_text, time_str, append); \

    #define ___copy_phoneme_id_to_raw_text(id, append) \
        sprintf(time_str, "%d", id); \
        raw_text += strcpy_ex(raw_text, time_str, append); \

    #define ___copy_text_to_raw_text(text, append) \
        raw_text += strcpy_ex(raw_text, text, append); \


        // VERSION 1.0
        ___copy_text_to_raw_text(phoneme->version, "\n");

        // PLAINTEXT
    #define PLAINTEXT_STR "PLAINTEXT"
        ___copy_text_to_raw_text(
                PLAINTEXT_STR, "\n"
                "{\n"
        );
        ___copy_text_to_raw_text(
                phoneme->PLAINTEXT.text, "\n"
                "}\n"
        );

        // WORDS
    #define WORDS_STR "WORDS"
        ___copy_text_to_raw_text(
                WORDS_STR, "\n"
                "{\n"
        );
        {
            // WORD
        #define WORD_STR "WORD "
            for (register size_t w = 0; w < phoneme->WORDS.num_words; w++) {
                ___copy_text_to_raw_text(WORD_STR, NULL);
                ___copy_text_to_raw_text(phoneme->WORDS.word[w].text, " ");
                ___copy_time_to_raw_text(phoneme->WORDS.word[w].start_time, PHONEME_START_TIME_AND_END_TIME_PRECISION, " ")
                ___copy_time_to_raw_text(phoneme->WORDS.word[w].end_time, PHONEME_START_TIME_AND_END_TIME_PRECISION, "\n{\n");
                // PHONEME
                for (register size_t p = 0; p < phoneme->WORDS.word[w].num_phonemes; p++) {
                    ___copy_phoneme_id_to_raw_text(phoneme->WORDS.word[w].phoneme[p].id, " ");
                    ___copy_text_to_raw_text(phoneme->WORDS.word[w].phoneme[p].name, " ");
                    ___copy_time_to_raw_text(phoneme->WORDS.word[w].phoneme[p].start_time, PHONEME_START_TIME_AND_END_TIME_PRECISION, " ");
                    ___copy_time_to_raw_text(phoneme->WORDS.word[w].phoneme[p].end_time, PHONEME_START_TIME_AND_END_TIME_PRECISION, " ");
                    *raw_text++ = phoneme->WORDS.word[w].phoneme[p]._1;
                    *raw_text++ = '\n';
                }
                *raw_text++ = '}';
                *raw_text++ = '\n';
            }
            *raw_text++ = '}';
            *raw_text++ = '\n';
        }


        // EMPHASIS
    #define EMPHASIS_STR "EMPHASIS"
        ___copy_text_to_raw_text(
                EMPHASIS_STR, "\n"
                "{\n"
        );
        for (register size_t e = 0; e < phoneme->EMPHASIS.num_emphasis; e++) {
            ___copy_time_to_raw_text(phoneme->EMPHASIS.emphasis[e].time, PHONEME_EMPHASIS_TIME_AND_NORMALIZED_VALUE_PRECISION, " ");
            ___copy_time_to_raw_text(phoneme->EMPHASIS.emphasis[e].normalised_value, PHONEME_EMPHASIS_TIME_AND_NORMALIZED_VALUE_PRECISION, "\n");
        }
        *raw_text++ = '}';
        *raw_text++ = '\n';


        // CLOSECAPTION
        // не используется
        //


        // OPTIONS
    #define OPTIONS_STR "OPTIONS"
        ___copy_text_to_raw_text(
                OPTIONS_STR, "\n"
                "{\n"
        );
        {
        #define voice_duck_str "voice_duck "
            ___copy_text_to_raw_text(voice_duck_str, NULL);
            *raw_text++ = phoneme->OPTIONS.voice_duck;
            *raw_text++ = '\n';
        }
        *raw_text++ = '}';
        *raw_text++ = '\n';

        *raw_text = '\0';

        // debug
        //printf("'%s'", raw_phoneme->text);
        //if (strlen(raw_phoneme->text) == raw_phoneme->vdat_chunk.size) ___debug_msg(1);
        //

        return PHONEME_OK;
    } else  {
        return PHONEME_MEMORY_ALLOCATION_ERROR;
    }
}



#define ___retime(old_time, old_duration, new_duration)         (old_time * new_duration) / old_duration;
void RetimePhoneme( _in_out_ PHONEME * phoneme,
                    _in_ const double old_wave_duration, _in_ const double new_wave_duration )
{
    // WORD
    for (register size_t w = 0; w < phoneme->WORDS.num_words; w++) {
        phoneme->WORDS.word[w].start_time = ___retime(phoneme->WORDS.word[w].start_time, old_wave_duration, new_wave_duration);
        phoneme->WORDS.word[w].end_time = ___retime(phoneme->WORDS.word[w].end_time, old_wave_duration, new_wave_duration);
        // PHONEME
        for (register size_t p = 0; p < phoneme->WORDS.word[w].num_phonemes; p++) {
            phoneme->WORDS.word[w].phoneme[p].start_time = ___retime(phoneme->WORDS.word[w].phoneme[p].start_time, old_wave_duration, new_wave_duration);
            phoneme->WORDS.word[w].phoneme[p].end_time = ___retime(phoneme->WORDS.word[w].phoneme[p].end_time, old_wave_duration, new_wave_duration);
        }
    }

    // EMPHASIS
    for (register size_t e = 0; e < phoneme->EMPHASIS.num_emphasis; e++) {
        phoneme->EMPHASIS.emphasis[e].time = ___retime(phoneme->EMPHASIS.emphasis[e].time, old_wave_duration, new_wave_duration);
    }

    return;
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



/////////////////////////////////////////////////////////////////////////////////////////////////////////// Command Line
bool GetCommandLineArgumets( _in_out_ wchar_t * SrcWaveFileName,
                             _in_out_ wchar_t * DstWaveFileName,
                             _in_out_ wchar_t * OutWaveFileName,
                             _in_out_ bool * CleanOutWaveFile,
                             _in_out_ bool * Logging)
{
    bool res = false;
    bool s_arg = false;
    bool d_arg = false;

    wchar_t ** Arg;
    int ArgCount;
    Arg = CommandLineToArgvW(GetCommandLineW(), &ArgCount);
    if(Arg != NULL) {
        if (ArgCount == 1) { goto finish; }

        *OutWaveFileName = L'\0';
        *CleanOutWaveFile = false;
        *Logging = false;

        for(int x = 1; x < ArgCount; x++) {
            switch (Arg[x][1]) {
                case L's':
                    wcscpy(SrcWaveFileName, Arg[x + 1]);
                    s_arg = true;
                    break;

                case L'd':
                    wcscpy(DstWaveFileName, Arg[x + 1]);
                    d_arg = true;
                    break;

                case L'o':
                    wcscpy(OutWaveFileName, Arg[x + 1]);
                    *CleanOutWaveFile = (Arg[x][2] == L'c');
                    break;

                case L'l':
                    *Logging = true;
                    break;

                default:
                    break;
            }
        }

        res = true;

finish:
        LocalFree(Arg);
    }

    return (res && s_arg && d_arg);
}



const char * GetCommandLineArgumetsHelp() {
    return "\n============================ phoneme retiming HELP ============================\n"
            "[required]-s \"source wave file name\"\n"
            "   ---> source wave file with source phoneme on it's end\n"
            "\n"
            "[required]-d \"source wave file name\"\n"
            "   ---> destanation wave file for appending retimed phoneme\n"
            "\n"
            "[optional]-o[+optional]c \"output wave file name\"\n"
            "   ---> new output wave file (created from destanation wave file) for appending\n"
            "        retimed phoneme.'c' saves output file without any non wave data\n"
            "        (if it is presented on destanation's wave file end) on it's end\n"
            "\n"
            "[optional]-l\n"
            "   ---> enable logging\n"
            "\n"
            "example: -s \"C:\\source.wav\" -d \"C:\\destanation.wav\" -oc \"C:\\output.wav\" -l"
            "\n===============================================================================\n";
    /*
        [-s]        (обязательно) - оригинальный звуковой файл с фонемой для изменения по времени
        [-d]        (обязательно) - целевой звуковой файл, в который сохраняется изменённая по времени фонема
        [-o, -oc]   (опционально) - копия звукового файла из [-d](с доп. коммандой 'c' без лишних данных на конце), в который сохраняется изменённая по времени фонема
        [-l]        (опционально) - вкл. логирование
        пример: -s "C:\source.wav" -d "C:\destanation.wav" -oc "C:\output.wav" -l
    */
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



// логирование


int main() {
    // аргументы ком строки
    wchar_t SrcWaveFileName[MAX_PATH];
    wchar_t DstWaveFileName[MAX_PATH];
    wchar_t OutWaveFileName[MAX_PATH];
    bool CleanOutWaveFile;
    bool Logging;

    if ( ! GetCommandLineArgumets(SrcWaveFileName, DstWaveFileName, OutWaveFileName, &CleanOutWaveFile, &Logging)) {
        printf("\nWRONG commad line arguments\n%s\n", GetCommandLineArgumetsHelp());
        return EXIT_FAILURE;
    }



    // проверка наличия файлов



    if ( ! FileExist(SrcWaveFileName)) {
        wprintf(L"OPENING SOURCE WAVE FILE ---> \"%ls\" ---> ERROR: file not found\n", SrcWaveFileName);
        return EXIT_FAILURE;
    }

    if ( ! FileExist(DstWaveFileName)) {
        wprintf(L"OPENING DESTANATION WAVE FILE ---> destanation file; \"%ls\" ---> ERROR: file not found\n", DstWaveFileName);
        return EXIT_FAILURE;
    }



    // начало основных действий



    printf("\n\n\n");

    bool SrcWaveFileOpened = false;
    bool DstWaveFileOpened = false;
    bool SrcRawPhonemeLoaded = false;
    bool PhonemeCreated = false;
    bool DstRawPhonemeCreated = false;
    bool OutWaveFileOpened = false;

    WAVEFILEINFO SrcWaveInfo;
    if ( ! OpenWaveFile(SrcWaveFileName, &SrcWaveInfo)) {
        wprintf(L"OPENING SOURCE WAVE FILE ---> \"%ls\" ---> ERROR: file is not valid wave file\n", SrcWaveFileName);
        goto finish;
    } else {
        SrcWaveFileOpened = true;
    }

    WAVEFILEINFO DstWaveInfo;
    if ( ! OpenWaveFile(DstWaveFileName, &DstWaveInfo)) {
        wprintf(L"OPENING DESTANATION WAVE FILE ---> \"%ls\" ---> ERROR: file is not valid wave file\n", DstWaveFileName);
        goto finish;
    } else {
        DstWaveFileOpened = true;
    }

    wprintf(L"OPENING SOURCE WAVE FILE ---> \"%ls\" ---> OK\n", SrcWaveFileName);
    wprintf(L"OPENING DESTANATION WAVE FILE ---> \"%ls\" ---> OK\n", DstWaveFileName);

    if (Logging) {
        CreateLog();
        SaveAsciiTextToLog("\nSTART TIME: ");
        SaveAsciiTextToLog(GetFmtTimeStr());
        SaveAsciiTextToLog("\n");
        SaveAsciiTextToLog("source wave file: \""); SaveWideTextToLog(SrcWaveFileName); SaveAsciiTextToLog("\"\n");
        SaveAsciiTextToLog("destanation wave file: \""); SaveWideTextToLog(DstWaveFileName); SaveAsciiTextToLog("\"\n");
    }


    // преобразование текста фонемы в структуру, ретайм, преобразование обратно



    RAW_PHONEME SrcRawPhoneme;
    if ( ! LoadRawPhoneme(&SrcWaveInfo, &SrcRawPhoneme)) {
        wprintf(L"LOADING PHONEME FROM SOURCE WAVE FILE ---> ERROR: file has no phoneme\n");
        goto finish;
    } else {
        wprintf(L"LOADING PHONEME FROM SOURCE WAVE FILE ---> OK\n");
        SrcRawPhonemeLoaded = true;
    }

    PHONEME_RESULT phoneme_res;
    PHONEME Phoneme;
    phoneme_res = RawPhonemeToPhoneme(&SrcRawPhoneme, &Phoneme);
    printf("ANALYZING PHONEME ---> %s\n", GetPhonemeResultStr(phoneme_res)); // хотя тут не анализ а парсинг в структуру,
    if (phoneme_res != PHONEME_OK) {
        goto finish;
    } else {
        PhonemeCreated = true;
        printf("\n===================== source phoneme =====================\n");
        printf(SrcRawPhoneme.text);
        printf("==========================================================\n\n");

        if (Logging) {
            SaveAsciiTextToLog("\n===================== source phoneme =====================\n");
            SaveAsciiTextToLog(SrcRawPhoneme.text, SrcRawPhoneme.vdat_chunk.size);
            SaveAsciiTextToLog("==========================================================\n\n");
        }
    }
    UnloadRawPhoneme(&SrcRawPhoneme); // она больше не нужна
    SrcRawPhonemeLoaded = false;

    double SrcWaveFileDuration;
    SrcWaveFileDuration = GetWaveFileDuration(&SrcWaveInfo);
    printf("SOURCE WAVE FILE DURATION ---> %.3f sec.\n", SrcWaveFileDuration);
    if (Logging) {
        SaveAsciiTextToLog("source wave file duration: "); SaveDoubleToLog(SrcWaveFileDuration); SaveAsciiTextToLog(" sec.\n");
    }

    CloseWaveFile(&SrcWaveInfo); // тоже больше не нужен
    SrcWaveFileOpened = false;

    double DstWaveFileDuration;
    DstWaveFileDuration = GetWaveFileDuration(&DstWaveInfo);
    printf("DESTANATION WAVE FILE DURATION ---> %.3f sec.\n", DstWaveFileDuration);
    if (Logging) {
        SaveAsciiTextToLog("destanation wave file duration: "); SaveDoubleToLog(DstWaveFileDuration); SaveAsciiTextToLog(" sec.\n");
    }

    RetimePhoneme(&Phoneme, SrcWaveFileDuration, DstWaveFileDuration);

    RAW_PHONEME DstRawPhoneme;
    phoneme_res = PhonemeToRawPhoneme(&Phoneme, &DstRawPhoneme);
    printf("RETIMING PHONEME ---> %s\n", GetPhonemeResultStr(phoneme_res)); // хотя ретайминг уже прошёл и здесь создаётся текст новой фонемы
    if (phoneme_res != PHONEME_OK) {
        goto finish;
    } else {
        DstRawPhonemeCreated = true;
        printf("\n===================== retimed phoneme =====================\n");
        printf(DstRawPhoneme.text);
        printf("===========================================================\n\n");

        if (Logging) {
            SaveAsciiTextToLog("\n===================== retimed phoneme =====================\n");
            SaveAsciiTextToLog(DstRawPhoneme.text, DstRawPhoneme.vdat_chunk.size);
            SaveAsciiTextToLog("===========================================================\n\n");
        }
    }
    FreePhoneme(&Phoneme); // уже не нужна. дальше осталось только сохранить DstRawPhoneme
    PhonemeCreated = false;



    // сохранение



    WAVEFILEINFO OutWaveInfo;
    if (OutWaveFileName[0] != L'\0') {

        if ( ! CopyWaveFile(&DstWaveInfo, OutWaveFileName, CleanOutWaveFile)) {
            wprintf(L"CREATING OUTPUT WAVE FILE ---> \"%ls\" ---> ERROR\n", &OutWaveFileName);
            goto finish;
        } else {
            wprintf(L"CREATING OUTPUT WAVE FILE ---> \"%ls\" ---> OK\n", &OutWaveFileName);
            if (Logging) {
                SaveAsciiTextToLog("output wave file: \""); SaveWideTextToLog(OutWaveFileName); SaveAsciiTextToLog("\"\n");
            }

            CloseWaveFile(&DstWaveInfo); // больше не нужен
            DstWaveFileOpened = false;

            if ( ! OpenWaveFile(OutWaveFileName, &OutWaveInfo)) {
                wprintf(L"OPENHING OUTPUT WAVE FILE ---> \"%ls\" ---> ERROR: file is not valid wave file\n", &OutWaveFileName);
                goto finish;
            } else {
                OutWaveFileOpened = true;
                if ( ! SaveRawPhoneme(&DstRawPhoneme, OutWaveFileName)) {
                    wprintf(L"SAVING RETIMED PHONEME TO OUTPUT WAVE FILE ---> \"%ls\" ---> ERROR\n", &OutWaveFileName);
                    goto finish;
                } else {
                    wprintf(L"SAVING RETIMED PHONEME TO OUTPUT WAVE FILE ---> \"%ls\" ---> OK\n", &OutWaveFileName);
                }

                CloseWaveFile(&OutWaveInfo); // больше не нужен
            }
        }

    } else {

        if ( ! SaveRawPhoneme(&DstRawPhoneme, DstWaveFileName)) {
            wprintf(L"SAVING RETIMED PHONEME TO DESTANATION WAVE FILE ---> \"%ls\" ---> ERROR\n", &DstWaveFileName);
            goto finish;
        } else {
            wprintf(L"SAVING RETIMED PHONEME TO DESTANATION WAVE FILE ---> \"%ls\" ---> OK\n", &DstWaveFileName);

            CloseWaveFile(&DstWaveInfo); // больше не нужен;
        }

    }

    UnloadRawPhoneme(&DstRawPhoneme); // она больше не нужна

    if (Logging) {
        SaveAsciiTextToLog("END TIME: ");
        SaveAsciiTextToLog(GetFmtTimeStr());
        SaveAsciiTextToLog("\n");
        CloseLog();
    }


    // всё зашибись!



    return EXIT_SUCCESS;



finish:
    if (SrcWaveFileOpened) { CloseWaveFile(&SrcWaveInfo); }
    if (DstWaveFileOpened) { CloseWaveFile(&DstWaveInfo); }
    if (SrcRawPhonemeLoaded) { UnloadRawPhoneme(&SrcRawPhoneme); }
    if (PhonemeCreated) { FreePhoneme(&Phoneme); }
    if (DstRawPhonemeCreated) { UnloadRawPhoneme(&DstRawPhoneme); }
    if (OutWaveFileOpened) { CloseWaveFile(&OutWaveInfo); }

    if (Logging) { CloseLog(); }

    return EXIT_FAILURE;
}




























