﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm
{
    public static class JsonResultDataExtension
    {

        public static JsonResultData  SetSuccess  (this JsonResultData  result)
        {
            result.StatusCode = StatusCodes.SUCCESS;
            return result;
        }

        public static JsonResultData<T> SetSuccess<T>(this JsonResultData<T> result)
        {
            result.StatusCode = StatusCodes.SUCCESS;
            return result;
        }

        public static JsonResultData  SetFail  (this JsonResultData  result, string message = null)
        {
            result.StatusCode = StatusCodes.FAIL;
            result.Message = message;
            return result;
        }

        public static JsonResultData<T> SetFail<T>(this JsonResultData<T> result, string message = null)
        {
            result.StatusCode = StatusCodes.FAIL;
            result.Message = message;
            return result;
        }


        public static JsonResultData  SetStatusCode (this JsonResultData result,int statusCode)
        {
            result.StatusCode = statusCode;
            return result;
        }

        public static JsonResultData<T> SetStatusCode<T>(this JsonResultData<T> result, int statusCode)
        {
            result.StatusCode = statusCode;
            return result;
        }

        public static JsonResultData<T> ToJsonResultData<T>(this T obj)
        {
            var count = obj is System.Collections.IEnumerable ? (obj as System.Collections.IEnumerable).Cast<object>().Count() : 0;
            return new JsonResultData<T> {Count=count, Data = obj, StatusCode = StatusCodes.SUCCESS };
        }



        public static JsonResultData<string> ToJson(this Exception ex)
        {
            return new JsonResultData<string> { Data =null, StatusCode = StatusCodes.ERROR, Message = ex.Message };
        }

        //public static JsonResultData  ToJson(this bool result,string message=null)
        //{
        //    return new JsonResultData  { Data = result, StatusCode =result? StatusCodes.SUCCESS:StatusCodes.FAIL,Message=message };
        //}

       

        public static JsonResultData<bool> ToJsonWithError(this string message)
        {
            return new JsonResultData<bool> { Data = false, Message = message, StatusCode = StatusCodes.ERROR };
        }

    }
}
