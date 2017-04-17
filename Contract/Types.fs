namespace Types

open System

type Quantity =
    | Items of float
    | Grams of int
    | Liters of float
    | TableSpoons of float
    | ToTaste
    

type Product =
    { name: string
    }

type ProductQuantity =
    { product: Product
    ; quantity: Quantity
    }

type Ingredient =
    | Only of ProductQuantity
    | Optional of ProductQuantity
    | Xor of Ingredient seq
    | And of Ingredient seq



type Step =
    { duration: TimeSpan
    ; description: string
    }

type Recipe =
    { steps: Step seq
    }

type Dish = 
    { name: string
    ; ingredients: Ingredient seq
    ; recipe: Recipe
    }