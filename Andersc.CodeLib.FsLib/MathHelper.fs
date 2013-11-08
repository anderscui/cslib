#light

module FsLib.MathHelper

open System

let pow x y = Math.Pow(x, y)

let square x = pow x 2.0
let cube x = pow x 3.0