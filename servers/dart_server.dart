import 'dart:convert';
import 'dart:io';
import 'dart:isolate';

const int _PORT = 9292;
const String _GREET = 'Hello World';

_startServer(arg) {
  List<int> response = UTF8.encode(_GREET);
  HttpServer.bind(InternetAddress.LOOPBACK_IP_V4, _PORT, shared:true).then((server) {
    server.listen((HttpRequest request) {
      request.response
        ..headers.contentType = new ContentType('text', 'plain')
        ..add(response)
        ..close();
    });
  });
}

void main() {
  final isolates = 4;
  for (int i = 0; i < isolates - 1; i++) {
    Isolate.spawn(_startServer, null);
  }
  _startServer(null);
}

