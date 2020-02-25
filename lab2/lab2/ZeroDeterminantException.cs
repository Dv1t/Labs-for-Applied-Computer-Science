using System;

namespace lab2
{
    class ZeroDeterminantException:Exception
    {
        private string _message = "Определитель матрицы равен нулю";
        override
        public string Message { get { return _message; } }
      
        override
        public string ToString()
        {
            return ($"{_message} {StackTrace}");
        }
    }
}
