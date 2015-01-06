﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkMapleLib
{
    /// <summary>
    /// Class containing constants found inside MapleStory
    /// </summary>
    /// <remarks>
    /// Created by Yaminike, aka Minike, aka 0minike0
    /// </remarks>
    internal static class Constants
    {
        /// <summary>
        /// AESKey
        /// </summary>
        internal static readonly UInt64 Key = 0x130806B41B0F3352;

        /// <summary>
        /// dwDefaultKey
        /// </summary>
        internal static readonly UInt32 DefaultKey = 0xC65053F2;

        /// <summary>
        /// Shuffle table used by MapleStory to shuffle the new IV
        /// </summary>
        internal static readonly byte[] Shuffle = new byte[0x100] { 
            0xEC, 0x3F, 0x77, 0xA4, 0x45, 0xD0, 0x71, 0xBF, 0xB7, 0x98, 0x20, 0xFC, 0x4B, 0xE9, 0xB3, 0xE1,
            0x5C, 0x22, 0xF7, 0x0C, 0x44, 0x1B, 0x81, 0xBD, 0x63, 0x8D, 0xD4, 0xC3, 0xF2, 0x10, 0x19, 0xE0,
            0xFB, 0xA1, 0x6E, 0x66, 0xEA, 0xAE, 0xD6, 0xCE, 0x06, 0x18, 0x4E, 0xEB, 0x78, 0x95, 0xDB, 0xBA,
            0xB6, 0x42, 0x7A, 0x2A, 0x83, 0x0B, 0x54, 0x67, 0x6D, 0xE8, 0x65, 0xE7, 0x2F, 0x07, 0xF3, 0xAA,
            0x27, 0x7B, 0x85, 0xB0, 0x26, 0xFD, 0x8B, 0xA9, 0xFA, 0xBE, 0xA8, 0xD7, 0xCB, 0xCC, 0x92, 0xDA,
            0xF9, 0x93, 0x60, 0x2D, 0xDD, 0xD2, 0xA2, 0x9B, 0x39, 0x5F, 0x82, 0x21, 0x4C, 0x69, 0xF8, 0x31,
            0x87, 0xEE, 0x8E, 0xAD, 0x8C, 0x6A, 0xBC, 0xB5, 0x6B, 0x59, 0x13, 0xF1, 0x04, 0x00, 0xF6, 0x5A,
            0x35, 0x79, 0x48, 0x8F, 0x15, 0xCD, 0x97, 0x57, 0x12, 0x3E, 0x37, 0xFF, 0x9D, 0x4F, 0x51, 0xF5,
            0xA3, 0x70, 0xBB, 0x14, 0x75, 0xC2, 0xB8, 0x72, 0xC0, 0xED, 0x7D, 0x68, 0xC9, 0x2E, 0x0D, 0x62,
            0x46, 0x17, 0x11, 0x4D, 0x6C, 0xC4, 0x7E, 0x53, 0xC1, 0x25, 0xC7, 0x9A, 0x1C, 0x88, 0x58, 0x2C,
            0x89, 0xDC, 0x02, 0x64, 0x40, 0x01, 0x5D, 0x38, 0xA5, 0xE2, 0xAF, 0x55, 0xD5, 0xEF, 0x1A, 0x7C,
            0xA7, 0x5B, 0xA6, 0x6F, 0x86, 0x9F, 0x73, 0xE6, 0x0A, 0xDE, 0x2B, 0x99, 0x4A, 0x47, 0x9C, 0xDF,
            0x09, 0x76, 0x9E, 0x30, 0x0E, 0xE4, 0xB2, 0x94, 0xA0, 0x3B, 0x34, 0x1D, 0x28, 0x0F, 0x36, 0xE3,
            0x23, 0xB4, 0x03, 0xD8, 0x90, 0xC8, 0x3C, 0xFE, 0x5E, 0x32, 0x24, 0x50, 0x1F, 0x3A, 0x43, 0x8A,
            0x96, 0x41, 0x74, 0xAC, 0x52, 0x33, 0xF0, 0xD9, 0x29, 0x80, 0xB1, 0x16, 0xD3, 0xAB, 0x91, 0xB9,
            0x84, 0x7F, 0x61, 0x1E, 0xCF, 0xC5, 0xD1, 0x56, 0x3D, 0xCA, 0xF4, 0x05, 0xC6, 0xE5, 0x08, 0x49
        };

        /// <summary>
        /// The crc table MapleStory uses
        /// </summary>
        internal static readonly UInt32[] Crc32Table = new UInt32[0x100] {
            0x00000000, 0x04C11DB7, 0x09823B6E, 0x0D4326D9, //0000000000
            0x130476DC, 0x17C56B6B, 0x1A864DB2, 0x1E475005, //0000000004
            0x2608EDB8, 0x22C9F00F, 0x2F8AD6D6, 0x2B4BCB61, //0000000008
            0x350C9B64, 0x31CD86D3, 0x3C8EA00A, 0x384FBDBD, //000000000C
            0x4C11DB70, 0x48D0C6C7, 0x4593E01E, 0x4152FDA9, //0000000010
            0x5F15ADAC, 0x5BD4B01B, 0x569796C2, 0x52568B75, //0000000014
            0x6A1936C8, 0x6ED82B7F, 0x639B0DA6, 0x675A1011, //0000000018
            0x791D4014, 0x7DDC5DA3, 0x709F7B7A, 0x745E66CD, //000000001C
            0x9823B6E0, 0x9CE2AB57, 0x91A18D8E, 0x95609039, //0000000020
            0x8B27C03C, 0x8FE6DD8B, 0x82A5FB52, 0x8664E6E5, //0000000024
            0xBE2B5B58, 0xBAEA46EF, 0xB7A96036, 0xB3687D81, //0000000028
            0xAD2F2D84, 0xA9EE3033, 0xA4AD16EA, 0xA06C0B5D, //000000002C
            0xD4326D90, 0xD0F37027, 0xDDB056FE, 0xD9714B49, //0000000030
            0xC7361B4C, 0xC3F706FB, 0xCEB42022, 0xCA753D95, //0000000034
            0xF23A8028, 0xF6FB9D9F, 0xFBB8BB46, 0xFF79A6F1, //0000000038
            0xE13EF6F4, 0xE5FFEB43, 0xE8BCCD9A, 0xEC7DD02D, //000000003C
            0x34867077, 0x30476DC0, 0x3D044B19, 0x39C556AE, //0000000040
            0x278206AB, 0x23431B1C, 0x2E003DC5, 0x2AC12072, //0000000044
            0x128E9DCF, 0x164F8078, 0x1B0CA6A1, 0x1FCDBB16, //0000000048
            0x018AEB13, 0x054BF6A4, 0x0808D07D, 0x0CC9CDCA, //000000004C
            0x7897AB07, 0x7C56B6B0, 0x71159069, 0x75D48DDE, //0000000050
            0x6B93DDDB, 0x6F52C06C, 0x6211E6B5, 0x66D0FB02, //0000000054
            0x5E9F46BF, 0x5A5E5B08, 0x571D7DD1, 0x53DC6066, //0000000058
            0x4D9B3063, 0x495A2DD4, 0x44190B0D, 0x40D816BA, //000000005C
            0xACA5C697, 0xA864DB20, 0xA527FDF9, 0xA1E6E04E, //0000000060
            0xBFA1B04B, 0xBB60ADFC, 0xB6238B25, 0xB2E29692, //0000000064
            0x8AAD2B2F, 0x8E6C3698, 0x832F1041, 0x87EE0DF6, //0000000068
            0x99A95DF3, 0x9D684044, 0x902B669D, 0x94EA7B2A, //000000006C
            0xE0B41DE7, 0xE4750050, 0xE9362689, 0xEDF73B3E, //0000000070
            0xF3B06B3B, 0xF771768C, 0xFA325055, 0xFEF34DE2, //0000000074
            0xC6BCF05F, 0xC27DEDE8, 0xCF3ECB31, 0xCBFFD686, //0000000078
            0xD5B88683, 0xD1799B34, 0xDC3ABDED, 0xD8FBA05A, //000000007C
            0x690CE0EE, 0x6DCDFD59, 0x608EDB80, 0x644FC637, //0000000080
            0x7A089632, 0x7EC98B85, 0x738AAD5C, 0x774BB0EB, //0000000084
            0x4F040D56, 0x4BC510E1, 0x46863638, 0x42472B8F, //0000000088
            0x5C007B8A, 0x58C1663D, 0x558240E4, 0x51435D53, //000000008C
            0x251D3B9E, 0x21DC2629, 0x2C9F00F0, 0x285E1D47, //0000000090
            0x36194D42, 0x32D850F5, 0x3F9B762C, 0x3B5A6B9B, //0000000094
            0x0315D626, 0x07D4CB91, 0x0A97ED48, 0x0E56F0FF, //0000000098
            0x1011A0FA, 0x14D0BD4D, 0x19939B94, 0x1D528623, //000000009C
            0xF12F560E, 0xF5EE4BB9, 0xF8AD6D60, 0xFC6C70D7, //00000000A0
            0xE22B20D2, 0xE6EA3D65, 0xEBA91BBC, 0xEF68060B, //00000000A4
            0xD727BBB6, 0xD3E6A601, 0xDEA580D8, 0xDA649D6F, //00000000A8
            0xC423CD6A, 0xC0E2D0DD, 0xCDA1F604, 0xC960EBB3, //00000000AC
            0xBD3E8D7E, 0xB9FF90C9, 0xB4BCB610, 0xB07DABA7, //00000000B0
            0xAE3AFBA2, 0xAAFBE615, 0xA7B8C0CC, 0xA379DD7B, //00000000B4
            0x9B3660C6, 0x9FF77D71, 0x92B45BA8, 0x9675461F, //00000000B8
            0x8832161A, 0x8CF30BAD, 0x81B02D74, 0x857130C3, //00000000BC
            0x5D8A9099, 0x594B8D2E, 0x5408ABF7, 0x50C9B640, //00000000C0
            0x4E8EE645, 0x4A4FFBF2, 0x470CDD2B, 0x43CDC09C, //00000000C4
            0x7B827D21, 0x7F436096, 0x7200464F, 0x76C15BF8, //00000000C8
            0x68860BFD, 0x6C47164A, 0x61043093, 0x65C52D24, //00000000CC
            0x119B4BE9, 0x155A565E, 0x18197087, 0x1CD86D30, //00000000D0
            0x029F3D35, 0x065E2082, 0x0B1D065B, 0x0FDC1BEC, //00000000D4
            0x3793A651, 0x3352BBE6, 0x3E119D3F, 0x3AD08088, //00000000D8
            0x2497D08D, 0x2056CD3A, 0x2D15EBE3, 0x29D4F654, //00000000DC
            0xC5A92679, 0xC1683BCE, 0xCC2B1D17, 0xC8EA00A0, //00000000E0
            0xD6AD50A5, 0xD26C4D12, 0xDF2F6BCB, 0xDBEE767C, //00000000E4
            0xE3A1CBC1, 0xE760D676, 0xEA23F0AF, 0xEEE2ED18, //00000000E8
            0xF0A5BD1D, 0xF464A0AA, 0xF9278673, 0xFDE69BC4, //00000000EC
            0x89B8FD09, 0x8D79E0BE, 0x803AC667, 0x84FBDBD0, //00000000F0
            0x9ABC8BD5, 0x9E7D9662, 0x933EB0BB, 0x97FFAD0C, //00000000F4
            0xAFB010B1, 0xAB710D06, 0xA6322BDF, 0xA2F33668, //00000000F8
            0xBCB4666D, 0xB8757BDA, 0xB5365D03, 0xB1F740B4  //00000000FC
        };
    }
}
