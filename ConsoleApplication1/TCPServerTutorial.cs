using System;
using System.Collections.Generic;
using System.Net;         //added
using System.Net.Sockets; //added
using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

namespace TCPServerTutorial {
    class TCPServer {
        private TcpListener tcpListner;
        private int tcpPort;
        
        private void SetTcpPort(int TcpPort) {
            this.tcpPort = TcpPort;

        }
        

        public TCPServer() {
            SetTcpPort(3000);
            this.tcpListner = new TcpListener(IPAddress.Any, this.tcpPort);
            this.tcpListner.Start();
            while (true) {
                // Принимаем новых клиентов
                 new ConectionHandler(this.tcpListner.AcceptTcpClient());
            }
        }

        ~TCPServer() {
            if (this.tcpListner != null) {
                this.tcpListner.Start();                
            }
            
        }
       
    }
    class ConectionHandler {
        public Encoding tcpEncoding {
            get { return _tcpEncoding; }
        }
        private Encoding _tcpEncoding = Encoding.Default;

        private void SetEncoding(Encoding tcpIncoding) {
            if (true) {
                // Дописать!!!!!
            }
            this._tcpEncoding = Encoding.GetEncoding("windows-1251");
        }
        // Конструктор класса. Ему нужно передавать принятого клиента от TcpListener
        public ConectionHandler(TcpClient Client) {
            // Код простой HTML-странички
            string Html = "<html><body><h1>Проверка на знание Великого и Могучего</h1></body></html>";
            // Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
            string Str = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
            // Приведем строку к виду массива байт
            //string asdasd = Encoding.Default.ToString();
            byte[] Buffer = this.tcpEncoding.GetBytes(Str);
            // Отправим его клиенту
            Client.GetStream().Write(Buffer, 0, Buffer.Length);
            // Закроем соединение
            Client.Close();
        }
    }
}
