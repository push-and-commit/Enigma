using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeritageEtInterface
{
    internal class Customer : User
    {
        private string _membershipDate;
        private List<MediaObject> mediaList = new List<MediaObject>();

        public Customer(string lastName, string firstName, int age, string membershipDate) : base(lastName, firstName, age)
        {
            _membershipDate = membershipDate;
        }

        public string MembershipDate { get => _membershipDate; set => _membershipDate = value; }
        internal List<MediaObject> MediaList { get => mediaList; set => mediaList = value; }

        public void AddMedia(MediaObject media) {
            mediaList.Add(media);
        }
        public void RemoveMedia(MediaObject media) {
            mediaList.Remove(media);
        }
    }
}
