# D6-rigged-die-simulation-in-Unity

A simple d6 die physics simulation in unity. 
By pressing the 'w' key on your keyboard the die is thrown and the result will (almost) always be 6. Press 'w' again to re-throw the die, or 'r' to reset.
This is achieved by first throwing an invisible die, with random angle, rotation and force, at accelerated time speed and waiting for it to stop moving. Once it stops we find what side is facing up and place the sides on the visible die such that when it's thrown with the same initial conditions it will generate a 6 as a result.
The reason why this has a small probability of failure is, likely, because the invisible die might stop moving in a frame (velocity = zero) but still be in an unstable state, i.e.: be stopped on one of its edges where the force of gravity will pull it down in the next frame. The probability of failure should be close to zero, although I have not conducted rigorous testing, I have observed it once in about 1000 trials.
