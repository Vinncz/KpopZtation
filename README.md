ㅤ
ㅤ
# Documentation of `KpopZtation`

ㅤ

__Made for:__
> _Pattern Software Design_,
> LAB -  Assesment of Learning
> 2023 Even Term

ㅤ

__Composed by:__
> 2501977941 - Kevin Gunawan
> 
> 2501968451 – Matthew Maximillian Tane
> 
> 2501967575 – Stevans Calvin Chandra
> 
> 2501977430 - Yofadha Chandra Berliano

ㅤ

__Notice board:__
\
This project has fulfilled its purpose per June 15th, 2023, and may never be updated again in the future. It may be kinda sad just leaving KpopZtation to rot, but it may serve as a reference to which DDD pattern can be applied in this sort of application.  

ㅤ

## Creating a New Page

The following code will enforce elements' visibility restriction, as per __#ID Naming Convention__, and is a __*must*__ have in every page.
```csharp
private static ElementController ec = new ElementController();
private static NavigationController nc = new NavigationController();

/* Place the Following inside [Page_Load] function */
AuthController.MakeSessionFromCookie();
ec.PrepareVisibility(Page, AuthController.ExtractCustomer());
```

ㅤ

While the following is customizable per page's need.
```csharp
/* Place the Following inside [Page_Load] function, after the "PrepareVisibility" fucntion is called */
nc.BlockIfNotAdmin(AuthController.ExtractCustomer(), "EditArtist.aspx");
```
or
```csharp
nc.BlockWhenSignedIn(AuthController.ExtractCustomer());
```
or
```csharp
nc.BlockIfNotBuyer(AuthController.ExtractCustomer());
```

ㅤ

## ID Naming Convention

_Access Modifier:_
|	 |	Admin Only	|	Buyer Only		|	Guest Only		|	No Restriction	|	Everyone but Admin		|	Everyone but Buyer |	Everyone but Guest |
|	--	|	--	|	--	|	--	|	--	|	--	|	--	|	--	|
|	Prefix				|	`AO`					| 	`BO`					|	`GO`					|	`__`						|	`_A`									|	`_B`									|	`_G`									|

ㅤ

_Element Identifier:_
|  							|	Textboxㅤ			|	Labelㅤ				|	Buttonㅤ			|	File Uploadㅤ		|	Repeaterㅤ	|	Divㅤ	|	Any Elementㅤ	|
|	---------------	|	-----------------	|	-----------------	|	-----------------	|	-----------------	|	-----------------	|	-----------------	|	-----------------	|
|	Prefix				|	`TB`					| 	`LB`					|	`BT`					|	`FU`					|	`RE`						|	`DV`						|	`__`						|

ㅤ

_Order of Usage:_

[1] `Access Modifier` first, then followed by [2]`Type Identifier`, and lastly by the [3]`Element's Name` itself.

ㅤ

*Use case example:*
|  										|	Textboxㅤ		|	Labelㅤ			|	Buttonㅤ		|	File Uploadㅤ	|	Any Element	|
|	----------------------	|	---------------	|	---------------	|	---------------	|	---------------	|	--------------	|
|	Admin Only				|	`AOTB`			| 	`AOLB`			|	`AOBT`			|	`AOFU`			|	`AO__`			|
|	Buyer Only					|	`BOTB`			| 	`BOLB`			|	`BOBT`			|	`BOFU`			|	`BO__`			|
|	Guest Only					|	`GOTB`			| 	`GOLB`			|	`GOBT`			|	`GOFU`			|	`GO__`			|
|	Everyone but Admin	|	`_ATB`			|  `_ALB`				|	`_ABT`			|	`_AFU`			|	`_A__`				|
|	Everyone but Buyer	|	`_BTB`			|  `_BLB`				|	`_BBT`			|	`_BFU`			|	`_B__`				|
|	Everyone but Guest	|	`_GTB`			|  `_GLB`				|	`_GBT`			|	`_GFU`			|	`_G__`				|
|	No Restriction			|	`__TB`			|  `__LB`				|	`__BT`				|	`__FU`			|	`____`				|

ㅤ

## Page Documentation
### Home.aspx
- Entry point of KPopZtation
- Displays artists whose music are for sale
- Guests can view an artist or register/sign into an account
- If logged in as a buyer, options to view their cart, transaction history and update their profile becomes visible in the navigation bar
- If logged in as an Admin, options to add, delete or edit an artist becomes visible
### ViewArtist.aspx
- Displays albums by artists that are for sale
- Led to this page by clicking one of the artist cards in `Home.aspx`
- Guests can view the album information by clicking on the card
- If logged in as an Admin, options to add, delete or edit an album becomes visible
### ViewAlbum.aspx
- Displays album information (cover, title, artist, description, price and available stock)
- Led to this page by clicking one of the album cards in `ViewArtist.aspx`
- Customers can add specified amounts of stock into their cart
- If customer attempts to buy more than available stock, an error message becomes visible
- If logged in as an Admin, an error message becomes visible (stating that Admins are not allowed to add albums to cart)
### Login.aspx
- Allows users to sign in through email and password
- Led to this page by clicking the sign in button in the navigation bar
- If remember me is checked, the website will set a cookie to automatically login for the next 7 days
- If inputted credentials are wrong or empty, an error message will be visible
### Register.aspx
- Allows users to register a new account
- Led to this page by clicking the register button in the navigation bar
- Receives full name, email, sex, address and password
- If inputted information is wrong or empty, an error message will be visible
### Cart.aspx
- Displays items that customer has added to their cart
- Led to this page by clicking the `My Cart` button in the navigation bar
- Subtotal and total items of all items in the cart is displayed
- Customer can remove items in their cart
- Customer can check out the items in their cart
- If cart is empty, a message will be visible
### TransactionHistory.aspx
- Displays transaction history
- Led to this page by clicking `Transaction History` button in the navigation bar
- Displays transaction ID, date, customer's name and the courier
- Customers can see the item they bought by clicking on the album card, which will lead to `ViewAlbum.aspx`
### UpdateProfile.aspx
- Allows customers and admins to update their profile information
- Led to this page by clicking `Update Profile` button in the navigation bar
- If inputted information is wrong or empty, an error message will be visible
### EditArtist.aspx
- Allows admins to edit artist information
- Led to this page by clicking the `Edit` button on an artist card in `Home.aspx`
- Artist's full name and profile picture can be edited
- If fields are empty, an error message will be visible
### AddArtist.aspx
- Allows admins to add new artists
- Led to this page by clicking the `Add New Artist` button in `Home.aspx`
- Artist's full name and profile picture can be edited
- If fields are empty, an error message will be visible
### EditAlbum.aspx
- Allows admins to edit album information
- Led to this page by clicking the `Edit` button on an album card in `ViewArtist.aspx`
- Album name, description, price, stock and cover can be edited
- If fields are empty, an error message will be visible
### AddAlbum.aspx
- Allows admins to add new albums
- Led to this page by clicking the `Add New Album` button on an album card in `ViewArtist.aspx`
- Album name, description, price, stock and cover can be added
- If fields are empty, an error message will be visible
### TransactionReport.aspx
- Allows admins to generate a Crystal Report of past transaction done on the website
- Led to this page by clicking the `Transaction Report` button on the navigation bar
- Transaction ID, Customer ID, Transaction Date, Album Name, Quantity and Album Price are generated
