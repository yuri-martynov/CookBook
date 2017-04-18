module Operators

open System
open Types

// products & ingredients

let ($) (product: Product) (quantity: Quantity) =
    Only {product = product; quantity = quantity}
    
let (@) part product =
    Part (product, part)

let (&&&) (i1: Ingredient) (i2: Ingredient) =
    And (seq { yield i1; yield i2 })

let (^^^) (i1: Ingredient) (i2: Ingredient) =
    Xor (seq { yield i1; yield i2 })

// steps
let (++) (mins: int) (description: string) : Step =
    { duration = TimeSpan.FromMinutes (float mins)
    ; action = Cook description
    }

let (--) (mins: int) (description: string) : Step =
    { duration = TimeSpan.FromMinutes (float mins)
    ; action = Background description
    }