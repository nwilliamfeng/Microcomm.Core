using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Microcomm
{
    public class JsonResultData 
    {
        public int StatusCode { get; set; }

        private object _data = null;
        public object Data
        {
            get { return this._data; }
            set
            {
                this.SetData(value);
            }
        }

        public string Message { get; set; }

        
        protected virtual void SetData(object data)
        {
            this._data = data;
        }

     
    }


    
}