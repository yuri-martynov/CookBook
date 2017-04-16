namespace Types

open System

type Quantity =
    | Items of float
    | Gramm of float
    | Liters of float
    | Spoons of float

type Product =
    { name: string
    ; quantity: Quantity
    }

type Dish = 
    { name: string
    ; time: TimeSpan
    ; ingredients: Product list
    ; recipe: string
    }


