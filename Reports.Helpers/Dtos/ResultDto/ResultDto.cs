using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Helpers.Dtos.ResultDto
{
    public class ResultDto
    {
        public ResultDto(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }

    public class ResultDto<T> : ResultDto
    {
        public ResultDto(bool succeeded, string message) : base(succeeded, message)
        {

        }
        public ResultDto(bool succeeded, string message, T data) : base(succeeded, message)
        {
            Data = data;
        }

        public T? Data { get; set; }

    }
}
