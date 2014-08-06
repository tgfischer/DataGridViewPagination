using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagedDataGridViewForm
{
    class PagedDataGridViewAdapterException : Exception
    {
        public PagedDataGridViewAdapterException() { }

        public PagedDataGridViewAdapterException(string message) : base(message) { }

        public PagedDataGridViewAdapterException(string message, System.Type fieldType)
            : base(message)
        {
            this.FieldType = fieldType;
        }

        public System.Type FieldType;
    }
}
