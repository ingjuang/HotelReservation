using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Domain.ValueObjects
{
    public record GuestDocument
    {
        public string DocumentType { get; init; }
        public string DocumentNumber { get; init; }

        protected GuestDocument(string documentType, string documentNumber)
        {
            DocumentType = documentType;
            DocumentNumber = documentNumber;
        }

        public static GuestDocument Create(string documentType, string documentNumber)
        {
            Validate(documentType, documentNumber);
            return new GuestDocument(documentType, documentNumber);
        }

        private static void Validate(string documentType, string documentNumber)
        {
            if (string.IsNullOrWhiteSpace(documentType))
            {
                throw new ArgumentException("Document type cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(documentNumber))
            {
                throw new ArgumentException("Document number cannot be empty");
            }
        }
    }
}
