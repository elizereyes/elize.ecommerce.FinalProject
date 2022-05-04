Feature: Add an item to the cart and apply the discount

Background: 
	Given these details
	| firstName | lastName | streetAddress     | city    | postCode | phoneNo    |
	| Elize     | Reyes    | 123 Nfocus Street | Telford | TF2 9FT  | 0712345678 |


@Discount
Scenario: Add an item to cart and apply the discount
	Given that I am logged in
	When I add an item into my cart
	* provide a discount code
	Then my total should update correctly


@Checkout
Scenario: Add an item to cart, checkout and complete the billing details
	Given that I am logged in
	When I add an item into my cart
	And I provide valid billing details
	Then my order should show up in the order history
	