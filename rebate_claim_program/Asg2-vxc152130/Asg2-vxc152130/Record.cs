using System;

namespace Asg2_vxc152130
{
    /**
     *  tw.WriteLine("FName \tLName \tMInit \tAddr1 \tAddr2 \tCity \tState \tZip \tPhone \tEmail \tIsProof \tReceivedDate \tRecordStart \tRecordEnd");

     * */
    internal class Record : IEquatable<Record>
    {
        private string recordString;
        private string fName;
        private string lName;
        private string minit;
        private string fullName;
        private string addr1;
        private string addr2;
        private string city;
        private string state;
        private string zip;
        private string phone;
        private string email;
        private bool proof;
        private DateTime receivedDate;
        private string startTime;
        private string endTime;

        /**
         * For read line from config file
         */
        public Record(string recordString, string fName, string lName, string minit,
            string addr1, string addr2, string city, string state, string zip,
            string phone, string email, bool proof, DateTime receivedDate,
            string startTime, string endTime)
        {
            this.recordString = recordString;
            this.fName = fName;
            this.lName = lName;
            this.Minit = minit;
            this.fullName = fName + " " + lName + " " + minit;
            this.addr1 = addr1;
            this.addr2 = addr2;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phone = phone;
            this.email = email;
            this.proof = proof;
            this.receivedDate = receivedDate;
            this.startTime = startTime;
            this.endTime = endTime;
        }
        /**
         * For add new record
         */
        public Record(string fName, string lName, string minit,
            string addr1, string addr2, string city, string state, string zip,
            string phone, string email, bool proof, DateTime receivedDate,
            string startTime, string endTime)
        {
            this.fName = fName;
            this.lName = lName;
            this.Minit = minit;
            this.fullName = fName + " " + lName + " " + minit;
            this.addr1 = addr1;
            this.addr2 = addr2;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phone = phone;
            this.email = email;
            this.proof = proof;
            this.receivedDate = receivedDate;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public string Minit { get => minit; set => minit = value; }
        public string RecordString { get => recordString; set => recordString = value; }
        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string Minit1 { get => minit; set => minit = value; }
        public string Addr1 { get => addr1; set => addr1 = value; }
        public string Addr2 { get => addr2; set => addr2 = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string Zip { get => zip; set => zip = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public bool Proof { get => proof; set => proof = value; }
        public DateTime ReceivedDate { get => receivedDate; set => receivedDate = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }
        public string FullName { get => fullName; set => fullName = value; }

        public override string ToString()
        {
            return fName + "\t" + lName + "\t" + minit + "\t"
                + addr1 + "\t" + addr2 + "\t" + city + "\t" + state + "\t" + zip + "\t"
                + phone + "\t" + email + "\t" + proof + "\t" + String.Format("{0:MM/dd/yyyy}", receivedDate) + "\t" + startTime + "\t" + endTime;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Record);
        }

        public bool Equals(Record other)
        {
            return other != null &&
                   fName == other.fName &&
                   lName == other.lName &&
                   minit == other.minit;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}