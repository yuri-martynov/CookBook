namespace Types

open System

type Quantity =
    | Items of float
    | Grams of float
    | Liters of float
    | TableSpoons of float

type Product =
    { name: string
    }

type Ingredient =
    { product: Product
    ; quantity: Quantity
    }

type Dish = 
    { name: string
    ; time: TimeSpan
    ; ingredients: Ingredient seq
    ; recipe: string
    }


