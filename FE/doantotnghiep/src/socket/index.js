const app = require("express")();
var http = require("http").createServer(app);
var socket = require("socket.io")(http, {
  allowEIO3: true,
  cors: {
    origin: "*",
    methods: "*",
    credentials: true
  }
});

app.all("*", function (req, res, next) {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "X-Requested-With");
  res.header("Access-Control-Allow-Methods", "PUT,POST,GET,DELETE,OPTIONS");
  res.header("X-Powered-By", " 3.2.1");
  res.header("Content-Type", "application/json;charset=utf-8");
  next();
});

socket.on("connection", function (socket) {
  console.log("The client has a connection");

  socket.on("connect", () => {
    console.log("Client begins to connect");
  });

  socket.on("disconnect", () => {
    console.log("Client disconnect");
  });

  socket.on("send", (data) => {
    // When a data received from .Net Core, send that data into Vue
    console.log("Receiving data sent by .Net Core", data);
    socket.emit("receive",data);
  });
  
   socket.on("receive", (data) => {
    console.log("Vue Received: ", data);
  });
});

http.listen(3000, function () {
  console.log("server runing at 127.0.0.1:3000");
});