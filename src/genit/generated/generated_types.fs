module generated_types

type Register =
  {
    UserID : int64
    FirstName : string
    LastName : string
    Email : string
    Password : string
  }

type Login =
  {
    UserID : int64
    Email : string
    Password : string
  }

type Product =
  {
    ProductID : int64
    Name : string
    Description : string
    Price : double
    Category : string
  }

type Cart =
  {
    CartID : int64
    RegisterFK : int64
  }

type CartItem =
  {
    CartItemID : int64
    CartFK : int64
    ProductFK : int64
  }

type Checkout =
  {
    CheckoutID : int64
    CartFK : int64
  }
