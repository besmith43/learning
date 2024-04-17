// This provides the frozen (compiled bytecode) files that are included if
// any.
#include <Python.h>

#include "nuitka/constants_blob.h"

// Blob from which modules are unstreamed.
#define stream_data constant_bin

// These modules should be loaded as bytecode. They may e.g. have to be loadable
// during "Py_Initialize" already, or for irrelevance, they are only included
// in this un-optimized form. These are not compiled by Nuitka, and therefore
// are not accelerated at all, merely bundled with the binary or module, so
// that CPython library can start out finding them.

struct frozen_desc {
    char const *name;
    ssize_t start;
    int size;
};

void copyFrozenModulesTo( struct _frozen *destination )
{
    struct frozen_desc frozen_modules[] = {
        { "base64", 6351310, 11253 },
        { "codecs", 6362563, 36649 },
        { "copy_reg", 6399212, 5064 },
        { "encodings", 6404276, -4362 },
        { "encodings.aliases", 6408638, 8760 },
        { "encodings.ascii", 6417398, 2253 },
        { "encodings.base64_codec", 6419651, 3829 },
        { "encodings.big5", 6423480, 1748 },
        { "encodings.big5hkscs", 6425228, 1788 },
        { "encodings.bz2_codec", 6427016, 4721 },
        { "encodings.charmap", 6431737, 3465 },
        { "encodings.cp037", 6435202, 2829 },
        { "encodings.cp1006", 6438031, 2915 },
        { "encodings.cp1026", 6440946, 2843 },
        { "encodings.cp1140", 6443789, 2829 },
        { "encodings.cp1250", 6446618, 2866 },
        { "encodings.cp1251", 6449484, 2863 },
        { "encodings.cp1252", 6452347, 2866 },
        { "encodings.cp1253", 6455213, 2879 },
        { "encodings.cp1254", 6458092, 2868 },
        { "encodings.cp1255", 6460960, 2887 },
        { "encodings.cp1256", 6463847, 2865 },
        { "encodings.cp1257", 6466712, 2873 },
        { "encodings.cp1258", 6469585, 2871 },
        { "encodings.cp424", 6472456, 2859 },
        { "encodings.cp437", 6475315, 8064 },
        { "encodings.cp500", 6483379, 2829 },
        { "encodings.cp720", 6486208, 2926 },
        { "encodings.cp737", 6489134, 8292 },
        { "encodings.cp775", 6497426, 8078 },
        { "encodings.cp850", 6505504, 7811 },
        { "encodings.cp852", 6513315, 8080 },
        { "encodings.cp855", 6521395, 8261 },
        { "encodings.cp856", 6529656, 2891 },
        { "encodings.cp857", 6532547, 7801 },
        { "encodings.cp858", 6540348, 7781 },
        { "encodings.cp860", 6548129, 8047 },
        { "encodings.cp861", 6556176, 8058 },
        { "encodings.cp862", 6564234, 8193 },
        { "encodings.cp863", 6572427, 8058 },
        { "encodings.cp864", 6580485, 8189 },
        { "encodings.cp865", 6588674, 8058 },
        { "encodings.cp866", 6596732, 8293 },
        { "encodings.cp869", 6605025, 8105 },
        { "encodings.cp874", 6613130, 2957 },
        { "encodings.cp875", 6616087, 2826 },
        { "encodings.cp932", 6618913, 1756 },
        { "encodings.cp949", 6620669, 1756 },
        { "encodings.cp950", 6622425, 1756 },
        { "encodings.euc_jis_2004", 6624181, 1812 },
        { "encodings.euc_jisx0213", 6625993, 1812 },
        { "encodings.euc_jp", 6627805, 1764 },
        { "encodings.euc_kr", 6629569, 1764 },
        { "encodings.gb18030", 6631333, 1772 },
        { "encodings.gb2312", 6633105, 1764 },
        { "encodings.gbk", 6634869, 1740 },
        { "encodings.hex_codec", 6636609, 3781 },
        { "encodings.hp_roman8", 6640390, 4112 },
        { "encodings.hz", 6644502, 1732 },
        { "encodings.idna", 6646234, 6368 },
        { "encodings.iso2022_jp", 6652602, 1801 },
        { "encodings.iso2022_jp_1", 6654403, 1817 },
        { "encodings.iso2022_jp_2", 6656220, 1817 },
        { "encodings.iso2022_jp_2004", 6658037, 1841 },
        { "encodings.iso2022_jp_3", 6659878, 1817 },
        { "encodings.iso2022_jp_ext", 6661695, 1833 },
        { "encodings.iso2022_kr", 6663528, 1801 },
        { "encodings.iso8859_1", 6665329, 2868 },
        { "encodings.iso8859_10", 6668197, 2883 },
        { "encodings.iso8859_11", 6671080, 2977 },
        { "encodings.iso8859_13", 6674057, 2886 },
        { "encodings.iso8859_14", 6676943, 2904 },
        { "encodings.iso8859_15", 6679847, 2883 },
        { "encodings.iso8859_16", 6682730, 2885 },
        { "encodings.iso8859_2", 6685615, 2868 },
        { "encodings.iso8859_3", 6688483, 2875 },
        { "encodings.iso8859_4", 6691358, 2868 },
        { "encodings.iso8859_5", 6694226, 2869 },
        { "encodings.iso8859_6", 6697095, 2913 },
        { "encodings.iso8859_7", 6700008, 2876 },
        { "encodings.iso8859_8", 6702884, 2907 },
        { "encodings.iso8859_9", 6705791, 2868 },
        { "encodings.johab", 6708659, 1756 },
        { "encodings.koi8_r", 6710415, 2890 },
        { "encodings.koi8_u", 6713305, 2876 },
        { "encodings.latin_1", 6716181, 2283 },
        { "encodings.mac_arabic", 6718464, 8014 },
        { "encodings.mac_centeuro", 6726478, 2937 },
        { "encodings.mac_croatian", 6729415, 2945 },
        { "encodings.mac_cyrillic", 6732360, 2935 },
        { "encodings.mac_farsi", 6735295, 2849 },
        { "encodings.mac_greek", 6738144, 2889 },
        { "encodings.mac_iceland", 6741033, 2928 },
        { "encodings.mac_latin2", 6743961, 4907 },
        { "encodings.mac_roman", 6748868, 2906 },
        { "encodings.mac_romanian", 6751774, 2946 },
        { "encodings.mac_turkish", 6754720, 2929 },
        { "encodings.palmos", 6757649, 3066 },
        { "encodings.ptcp154", 6760715, 4890 },
        { "encodings.punycode", 6765605, 7942 },
        { "encodings.quopri_codec", 6773547, 3647 },
        { "encodings.raw_unicode_escape", 6777194, 2202 },
        { "encodings.rot_13", 6779396, 3656 },
        { "encodings.shift_jis", 6783052, 1788 },
        { "encodings.shift_jis_2004", 6784840, 1828 },
        { "encodings.shift_jisx0213", 6786668, 1828 },
        { "encodings.string_escape", 6788496, 2061 },
        { "encodings.tis_620", 6790557, 2938 },
        { "encodings.undefined", 6793495, 2589 },
        { "encodings.unicode_escape", 6796084, 2150 },
        { "encodings.unicode_internal", 6798234, 2176 },
        { "encodings.utf_16", 6800410, 5160 },
        { "encodings.utf_16_be", 6805570, 1990 },
        { "encodings.utf_16_le", 6807560, 1990 },
        { "encodings.utf_32", 6809550, 5724 },
        { "encodings.utf_32_be", 6815274, 1883 },
        { "encodings.utf_32_le", 6817157, 1883 },
        { "encodings.utf_7", 6819040, 1883 },
        { "encodings.utf_8", 6820923, 1942 },
        { "encodings.utf_8_sig", 6822865, 4977 },
        { "encodings.uu_codec", 6827842, 4909 },
        { "encodings.zlib_codec", 6832751, 4641 },
        { "functools", 6837392, 5996 },
        { "locale", 6843388, 54968 },
        { "quopri", 6898356, 6544 },
        { "re", 6904900, 13363 },
        { "sre_compile", 6918263, 12521 },
        { "sre_constants", 6930784, 6177 },
        { "sre_parse", 6936961, 19937 },
        { "string", 6956898, 20349 },
        { "stringprep", 6977247, 14439 },
        { "struct", 6991686, 229 },
        { "types", 6991915, 2703 },
        { NULL, 0, 0 }
    };

    struct frozen_desc *current = frozen_modules;

    for(;;)
    {
        destination->name = (char *)current->name;
        destination->code = (unsigned char *)&constant_bin[ current->start ];
        destination->size = current->size;

        if (destination->name == NULL) break;

        current += 1;
        destination += 1;
    };
}
