ㅤ
ㅤ
# Documentation of `KpopZtation`

__Made for:__
> _Pattern Software Design_,
> LAB -  Assesment of Learning
> 2023 Even Term

__Composed by:__
> 2501977941 - Kevin Gunawan

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

More sections to come
Selesai uts, gw jamin bakal nambah (i hope).
