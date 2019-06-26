using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Microcomm
{
    public class JsonResultData<T>:JsonResultData       
    {

        public JsonResultData()
        {
            this.Data = default(T);
        }

        public new T Data
        {
            get
            {
                if (base.Data == null)
                    return default(T);
                return (T)base.Data ;
            }
            set
            {
                base.Data = value;
            }
        }

        protected override void SetData(object data)
        {
            if (data == null)
            {
                base.SetData(default(T));
                return;
            }
            if (!(data is T))
                return;
            base.SetData(data);
        }



    }


    
}