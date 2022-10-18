using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.WinApiNative
{

    public enum CtkGpStatus
    {
        Ok = 0, //操作成功完成
        GenericError = 1, //一般性錯誤
        InvalidParameter = 2, //參數不正確
        OutOfMemory = 3, //記憶體不足
        ObjectBusy = 4, //對象沒準備好
        InsufficientBuffer = 5,
        NotImplemented = 6, //此操作尚未實現
        Win32Error = 7,
        WrongState = 8,
        Aborted = 9,
        FileNotFound = 10, //檔案沒找到
        ValueOverflow = 11,
        AccessDenied = 12,
        UnknownImageFormat = 13, //未知的檔案格式
        FontFamilyNotFound = 14, //字型沒找到
        FontStyleNotFound = 15,
        NotTrueTypeFont = 16,
        UnsupportedGdiplusVersion = 17,
        GdiplusNotInitialized
    }

}
