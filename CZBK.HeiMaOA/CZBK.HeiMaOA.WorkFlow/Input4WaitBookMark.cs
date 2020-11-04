using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace CZBK.HeiMaOA.WorkFlow
{

    public sealed class Input4WaitBookMark<T> :NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InOutArgument<string> BookMark { get; set; }
        public OutArgument<int> SetpId { get; set; }
        public OutArgument<T> Result { get; set; }
        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        //protected override void Execute(CodeActivityContext context)
        //{
        //    // 获取 Text 输入参数的运行时值
        //    string text = context.GetValue(this.Text);
        //}
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {
            string bookMarkName = context.GetValue(BookMark);
            context.CreateBookmark(bookMarkName, ContinExecuteWorkFlow);
        }
        public void ContinExecuteWorkFlow(NativeActivityContext context, Bookmark bookmark, object value)
        {

            var data = (ResaBookMarkObject<T>)value;
            context.SetValue(BookMark, data.BookMarkName);
            context.SetValue(SetpId,data.StepId);
            context.SetValue(Result,data.Result);
        }
    }
}
