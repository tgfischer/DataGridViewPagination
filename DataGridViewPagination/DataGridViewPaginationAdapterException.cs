using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridViewPagination
{
    class DataGridViewPaginationAdapterException : Exception
    {
        public DataGridViewPaginationAdapterException() { }

        public DataGridViewPaginationAdapterException(string message) : base(message) { }

        public DataGridViewPaginationAdapterException(string message, System.Type fieldType)
            : base(message)
        {
            this.FieldType = fieldType;
        }

        public System.Type FieldType;
    }
}
