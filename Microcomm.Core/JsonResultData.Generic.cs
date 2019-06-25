using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Microcomm
{
    public class JsonResultData<T>:JsonResultData       
    {
       
        public new T Data
        {
            get { return (T)base.Data ; }
            set
            {
                base.Data = value;
            }
        }

        
     
    }


    
}