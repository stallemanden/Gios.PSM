using System;
using System.Collections.Generic;
using System.Text;

namespace Gios.PDF.SplitMerge
{
    public class PdfMergerProgressEventArgs : EventArgs
    {
        public PdfMergerProgressEventArgs()
        {

        }

        public PdfMergerProgressEventArgs(int element, int completedPages, int totalPages, string operation)
        {
            this.Completed = completedPages;
            this.Total = totalPages;
            this.Element = element;
            this.Operation = operation;
        }

        private int _element;
        public int Element
        {
            get { return _element; }
            set { _element = value; }
        }

        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set { _operation = value; }
        }


        private int _totalPages;
        public int Total
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        private int _completedPages;
        public int Completed
        {
            get { return _completedPages; }
            set { _completedPages = value; }
        }

        public override string ToString()
        {
            return
                //"Element #"+this.Element + ": "+
                string.Format(this.Operation, this.Completed, this.Total); ; 
        }
    }
}
