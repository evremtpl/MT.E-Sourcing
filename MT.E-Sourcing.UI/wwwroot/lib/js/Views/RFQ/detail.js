﻿//const { signalR } = require("../../../microsoft-signalr/signalr");

var conn = new signalR.HubConnectionBuilder().withUrl("http://localhost:8002/auctionhub").build();

var auctionId = document.getElementById("AuctionId").value;


//Disable sendbutton until connectin is established

document.getElementById("sendButton").disabled = true;

var groupName = "auction" + auctionId;

conn.start().then(function () {

    document.getElementById("sendButton").disabled = false;
    conn.invoke("AddToGroupAsync", groupName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
})

conn.on("Bids", function (user,bid) {
    AddBidToTable(user, bid);

});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("SellerUserName").value;
    var productId = document.getElementById("ProductId").value;
    var sellerUser = user;
    var bid = document.getElementById("exampleInputPrice").value;

    var sendBidRequest = {
        AuctionId=auctionId,
        ProductId=productId,
        SellerUserName=sellerUser,
        Price=parseFloat(bid).toString();
    }

    SendBid(sendBidRequest);
    event.preventDefault();
});

document.getElementById("finishButton").addEventListener("click", function (event) {



    var sendCompleteBidRequest = {
        AuctionId=auctionId,
     
    }

    SendCompleteBid(sendBidRequest);
    event.preventDefault();
});

function AddBidToTable(user, bid) {
    var str= "<tr>";
    str += "<td>" + user + "</td>";
    str += "<td>" + bid + "</td>";
    str += "</tr>";

    if ($('table > tbody > tr:first').length > 0) {
        $('table > tbody > tr:first').before(str);
    }
    else {
        $('.bidLine').append(str);
    }
}



function SendBid(model) {
    $.ajax({
        url: "/Auction/SendBid",
        type: "POST",
        data: model,
        success: function (response) {
            if (response.isSuccess) {
                document.getElementById("exampleInputPrice").value = "";
                conn.invoke("SendBidAsync", groupName, model.SellerUserName, model.Price).catch(function (err) {
                    return console.error(err.toString());

                });
            }

        },

        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }




    });
}

function SendCompleteBid(model) {
    $.ajax({
        url: "/Auction/CompleteBid",
        type: "POST",
        data: model,
        success: function (response) {
            if (response.isSuccess) {


                console.log("Process Successfully Completed");
            }
        },

        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }




    });
}