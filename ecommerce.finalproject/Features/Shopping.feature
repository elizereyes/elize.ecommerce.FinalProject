Feature: Add an item to the cart and apply the discount

Background: 
	Given these details
	| firstName | lastName | streetAddress     | city    | postCode | phoneNo    |
	| Elize     | Reyes    | 123 Nfocus Street | Telford | TF2 9FT  | 0712345678 |
	And I am logged in

	


@Discount
Scenario: Add an item to cart and apply the discount
	When I add an 'Hoodie with Logo' into my cart
	And provide 'edgewords' discount code
	Then my total should update correctly with a discount of '15'%


@Checkout
Scenario: Add an item to cart, checkout and complete the billing details
	When I add an 'Hoodie with Logo' into my cart
	And I provide valid billing details
	Then my order should show up in the order history
