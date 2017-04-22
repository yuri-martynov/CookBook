namespace Products

open Types

[<AutoOpen>]
module GlobalSpices =
    let соль = Whole "соль"
    let перец = Whole "перец"
    let перец_горошек = Whole "перец чёрный горошек"
    let зелень = Whole "зелень"
    let чеснок = Whole "чеснок"
    let головка_чеснока = Part(чеснок, "зубчик")
    let ``лавровый лист`` = Whole "лавровый лист"

    let сахар = Whole "сахар"

    let разрыхлитель = Whole "разрыхлитель"

    let тимьян = Whole "тимьян"

    