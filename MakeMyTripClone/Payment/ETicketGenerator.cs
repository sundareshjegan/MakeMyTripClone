using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    class ETicketGenerator
    {
        public string GenerateETicket(ETicket ticket)
        {
            string bookingId = ticket.BookingID;
            string journeyDate = ticket.JourneyDate;
            string departureTime = ticket.DepartureTime;
            string sourceCity = ticket.SourceCity;
            string destinationCity = ticket.DestinationCity;
            string busName = ticket.BusName;
            string contact = ticket.Contact;
            string[,] passengerDetails = ticket.PassengerDetails;
            string totalFare = ticket.TotalFare;
            string paymentMethod = ticket.PaymentMethod;

            StringBuilder htmlBuilder = new StringBuilder();

            // Start building the HTML
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<meta charset=\"UTF-8\">");
            htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            htmlBuilder.AppendLine("<title>Bus Booking E-Ticket - MakeMyTrip</title>");
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("body {");
            htmlBuilder.AppendLine("    font-family: Arial, sans-serif;");
            htmlBuilder.AppendLine("    background-color: #f4f4f4;");
            htmlBuilder.AppendLine("    margin: 0;");
            htmlBuilder.AppendLine("    padding: 0;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".ticket {");
            htmlBuilder.AppendLine("    max-width: 600px;");
            htmlBuilder.AppendLine("    margin: 20px auto;");
            htmlBuilder.AppendLine("    padding: 20px;");
            htmlBuilder.AppendLine("    background-color: #ffffff;");
            htmlBuilder.AppendLine("    border-radius: 10px;");
            htmlBuilder.AppendLine("    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".header {");
            htmlBuilder.AppendLine("    text-align: center;");
            htmlBuilder.AppendLine("    padding-bottom: 20px;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".logo {");
            htmlBuilder.AppendLine("    width: 200px;");
            htmlBuilder.AppendLine("    height: auto;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".info, .passenger-details, .payment-details {");
            htmlBuilder.AppendLine("    padding: 20px;");
            htmlBuilder.AppendLine("    background-color: #f9f9f9;");
            htmlBuilder.AppendLine("    border-radius: 8px;");
            htmlBuilder.AppendLine("    margin-bottom: 20px;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".passenger-details table {");
            htmlBuilder.AppendLine("    width: 100%;");
            htmlBuilder.AppendLine("    border-collapse: collapse;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".passenger-details th, .passenger-details td {");
            htmlBuilder.AppendLine("    padding: 10px;");
            htmlBuilder.AppendLine("    border-bottom: 1px solid #ddd;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine(".payment-details p {");
            htmlBuilder.AppendLine("    margin: 0;");
            htmlBuilder.AppendLine("}");
            htmlBuilder.AppendLine("</style>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<div class=\"ticket\">");
            htmlBuilder.AppendLine("<div class=\"header\">");
            htmlBuilder.AppendLine("<img class=\"logo\" src=\"https://upload.wikimedia.org/wikipedia/commons/thumb/6/61/Makemytrip_logo.svg/800px-Makemytrip_logo.svg.png\" alt=\"MakeMyTrip Logo\">");
            htmlBuilder.AppendLine("<h2>Bus Booking E-Ticket</h2>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("<div class=\"info\">");
            htmlBuilder.AppendLine($"<p><strong>Booking ID:</strong> {bookingId}</p>");
            htmlBuilder.AppendLine($"<p><strong>Date of Journey:</strong> {journeyDate}</p>");
            htmlBuilder.AppendLine($"<p><strong>Departure Time:</strong> {departureTime}</p>");
            htmlBuilder.AppendLine($"<p><strong>From:</strong> {sourceCity}</p>");
            htmlBuilder.AppendLine($"<p><strong>To:</strong> {destinationCity}</p>");
            htmlBuilder.AppendLine($"<p><strong>Bus Name:</strong> {busName}</p>");
            htmlBuilder.AppendLine($"<p><strong>Contact:</strong> {contact}</p>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("<div class=\"passenger-details\">");
            htmlBuilder.AppendLine("<h3>Passenger Details:</h3>");
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<thead>");
            htmlBuilder.AppendLine("<tr>");
            htmlBuilder.AppendLine("<th>Passenger Name</th>");
            htmlBuilder.AppendLine("<th>Seat Number</th>");
            htmlBuilder.AppendLine("<th>Gender</th>");
            htmlBuilder.AppendLine("<th>Age</th>");
            htmlBuilder.AppendLine("</tr>");
            htmlBuilder.AppendLine("</thead>");
            htmlBuilder.AppendLine("<tbody>");

            // Add passenger details dynamically
            for (int i = 0; i < passengerDetails.GetLength(0); i++)
            {
                htmlBuilder.AppendLine("<tr>");
                for (int j = 0; j < passengerDetails.GetLength(1); j++)
                {
                    htmlBuilder.AppendLine($"<td>{passengerDetails[i, j]}</td>");
                }
                htmlBuilder.AppendLine("</tr>");
            }

            htmlBuilder.AppendLine("</tbody>");
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("<div class=\"payment-details\">");
            htmlBuilder.AppendLine("<h3>Payment Details:</h3>");
            htmlBuilder.AppendLine($"<p><strong>Total Fare:</strong> {totalFare}</p>");
            htmlBuilder.AppendLine($"<p><strong>Payment Method:</strong> {paymentMethod}</p><br/><br/>");
            htmlBuilder.AppendLine($"<center><p>It's not about the destination, it's about the journey.</p>");
            htmlBuilder.AppendLine($"<center><strong><p>Happy Journey..😊</p></strong>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("</div>");
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }

        public void SendETicket(string email, string ETicketAsHTML)
        {
            // Convert HTML to PDF
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            PdfDocument document = htmlConverter.Convert(ETicketAsHTML, "");

            // Convert PDF document to bytes
            MemoryStream memoryStream = new MemoryStream();
            document.Save(memoryStream);
            memoryStream.Position = 0;

            // Create email message
            MailAddress fromAddress = new MailAddress("whitehatsundar@gmail.com", "Make my Trip");
            MailAddress toAddress = new MailAddress(email);
            const string fromPassword = "xpre wkhu tgmh fsyj";
            string subject = "ETicket - Make my Trip";
            string body = ETicketAsHTML;

            try
            {
                using (var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                })
                using (var message = new MailMessage(fromAddress.ToString(), toAddress.ToString())
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    // Attach PDF to email
                    message.Attachments.Add(new Attachment(memoryStream, "ETicket.pdf", "application/pdf"));
                    smtp.Send(message);
                    MessageBox.Show("Ticket Sent to your mail. Please keep it with your travel. \n                              Happy Journey..😊");
                    Application.Exit();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Clean up resources
                document.Close(true);
                memoryStream.Close();
            }
        }
    }
}
