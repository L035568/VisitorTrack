using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;
using Newtonsoft.Json;

namespace VisitorTracker.Models
{
    public class Visitor:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty(PropertyName = "id")]
        [PrimaryKey]
        public string Id { get; set; }


        private string _vehicleNumber;
        [MaxLength(255)]
        public string VehicleNumber
        {
            get { return _vehicleNumber; }
            set
            {
                if (_vehicleNumber == value)
                    return;

                _vehicleNumber = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(255)]
        public string Name { get; set; }

        private string _flatNumber;
        [MaxLength(255)]
        public string FlatNumber
        {
            get { return _flatNumber; }
            set
            {
                if (_flatNumber == value)
                    return;

                _flatNumber = value;
                OnPropertyChanged();
            }
        }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }

        [MaxLength(255)]
        public string TimeIn { get; set; }

        [MaxLength(255)]
        public string Signature { get; set; }

        private void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
