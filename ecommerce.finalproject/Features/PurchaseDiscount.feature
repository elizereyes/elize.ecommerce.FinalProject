Feature: Add an item to the cart and apply the discount

@Discount
Scenario: Add an item to cart and apply the discount
	Given that I am logged in
	When I add an item into my cart
	* provide a discount code
	Then my total should update correctly