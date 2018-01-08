namespace Asg5_vxc152130
{
    /**
     * Bean class to store matched line string and its line number
     * @author: Vincy Shrine
     * @net-id: vxc152130
     */ 
    class ListItem
    {

        private string line;
        private int lineNo = 0;

        // Constructor with matched line number and line 
        public ListItem(int lineNo, string line)
        {
            this.lineNo = lineNo;
            this.line = line;
        }

        /** Doesn't work in lower versions of .Net VS */
        // public int LineNo { get => lineNo; set => lineNo = value; }
        // public string Line { get => line; set => line = value; }

        public int getLineNo()
        {
            return lineNo;
        }

        public string getLine()
        {
            return line;
        }
    }
}
