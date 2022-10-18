using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.ContextFlow
{

    /// <summary>
    /// 物件生命週期定義, 基本上是 Init -> Load -> Unload -> Free.
    /// </summary>
    public interface ICtkContextFlow
    {
        int CtkCfBoot();
        int CtkCfLoad();
        int CtkCfUnload();
        int CtkCfFree();
    }




    /* <Note>
    /// 若與IDisposable並用, 需注意CfFree的定義
    /// IDispose.Dispose() 一般會執行自己的回收 GC.SuppressFinalize(this);
    /// 也就是真正釋放自己
    /// 
    /// 那CfFree應該釋放誰? Member 還是該包含 'this' ?
    /// 
    /// (1)
    /// 邏輯上來講, CfFree還在 'this' 程序中(雖然Dispose也是) 因此應該只釋放Member
    /// 因此解構子通常也是寫 this.Dispose(false);
    /// 不對自己進行回收, 畢竟解構子己在回收程序中
    /// 
    /// (2)
    /// 而Member釋放完以後, 其實也不應該留下Resource, 應該是會自然被GC回收
    /// 
    /// =>即
    /// CfFree 一般只要像解構子一樣, 釋放Member後, 由GC自行回收
    /// 而 Dispose(void) 則是給外面程式使用
    /// 
    /// </Note>*/

    /* <Note>
    /// Context Flow .CfFree, Dispose, Close 的順序?
    ///     結論: CfFree -> Dispose -> Close
    ///     
    /// 並非所有物件會實作Context Flow:
    /// 假設A物件有Close Function, B物件繼承A物件且實作Context Flow
    /// 那麼B物件應當在CfFree中, 呼叫Close以確保關閉
    /// 同理, Dispose也應當呼叫Close
    /// 且Close亦可被單獨執行
    /// 
    /// 那麼 CfFree 與 Dispose 的順序?
    /// CfFree理當被執行, 而 Dispose 可能由使用者或系統認定回收時 執行
    /// (1) 即:CfFree 應當呼叫 Dispose 代表由使用者認定回收
    /// 
    /// 那 Dispose 在系統認定回收時 是否要執行CfFree
    /// (1) Reject: Context Flow .CfFree 是使用者認定的行為, 不應由系統執行
    /// (2) Accept: 系統認定回收的物件一般不會強求存在, 因此可以執行 CfFree
    /// 衝突產生-> 暫定(1)為最佳解
    ///     因為(2)的可執行不代表要執行
    ///     且 即然己在Dispose釋放資源了, 也不用擔心沒執行CfFree
    ///     再者 若真存在需要 UnLoad -> Free 的物件, 你也應照程序完成
    /// 
    /// 
    /// </Note>*/


    /* [d20210211]
     * 物件的生命週期定義,
     * 基本上使用 IDisposable 要求撰寫釋放時要 Release 的資源, 即可正常的開啟關閉物件.
     * 但有時需要明確的定義物件生命週期, 以利使用者在明確的時間點進行 載入/卸載 與 初始化/釋放 的行為.
     * 此時可以利用這個介面類別
     */




}
