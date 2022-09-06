# self-checkout-machine-API

The application was written using Visual studio and it uses .NET 6.0 framework.

To use clone or fork the master branch and open the .sln file with Visual studio.
Run the Program.cs file to start the application and start making requests to the following endpoints:

https://localhost:7090/api/v1/Stock
https://localhost:7090/api/v1/Checkout
or
http://localhost:5090/api/v1/Stock
http://localhost:5090/api/v1/Checkout

Euro payment was implemented:
To make a request using Euro, one or both optional fields are needed in endpoint "api/v1/Checkout":
Example:
"insertedEuro" : {
    "10": 1
},
"insertedEuroCoins" : {
    "10": 4
}

Unfortunately, Euro validation is not yet implemented, so any euro bills and coins can be given.
