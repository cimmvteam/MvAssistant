using System;
using System.Collections.Generic;
using System.Text;

namespace CToolkitCs.v1_2.TriggerDiagram
{
    public class ReadMe
    {
        /* [d20210210]
         * Trigger Diagram 是一個 設計概念 亦可是自創的 Design Pattern
         * 
         * 概念1: 節點, 區塊 與 圖.
         * 每個 Node 專注於把 單一工作完成,
         * 用 Block 將多個 Node 串或並聯起來, 成為完整作業,
         * 用 Diagram 將所有作業打包起來, 形成一個 Purpose.
         * 
         * 概念2: 各節點相連.
         * 採用event連結, 雖然就只是物件與事件導向設計的基礎,
         * 卻也是相當乾淨俐落的方法.
         * 提供 類似事件觸發時呼叫的Function(OnXxxxx),
         * 可取名叫 DoXxxxx(Object sender, EventArgs ea), 作為 input
         * 事件註冊的 event 作為 output,
         * 那麼連結 Node 只要對 event(output) 註冊 Function (input) 即可連結起來,
         * 且可多對多.
         * 
         * 此資料夾 僅提供幾個 interface 留作參考,
         * 未來會寫範例輔助說明 及 介紹使用方式.
         * 但應該是不會提供 實作 或 應用 library,
         * 此 Pattern 是一個設計概念, 不會寫成 工具類別
         * 
         * 
         */


    }
}
