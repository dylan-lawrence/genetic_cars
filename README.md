### Genetic Cars

##### Introduction
Inspired by other Genetic Cars simulations and showcases of Genetic Algorithms I am attempting to create my own Genetic Cars sim.

##### Car Design
The cars themselves are designed very simply with a body and two wheels. Each car has a body in the shape of a polygon consisting of six vertices. The points are parsed and organized in such a way as to ensure the shape has no internal intersections.

The wheels are centered on two vertices. Due to randomized gene the wheels could theoretically be placed on the same vertex.

##### Genome Basics
The Genome we will be using consists of four *nucleotides* that are then mapped to numbers and converted. These mappings are
* A -> 0
* T -> 1
* C -> 2
* G -> 3

We consider a *gene* to be a sequence of four letters **i.e. ATTC** This gene is converted first to a sequence of numbers and then to a final number.

<b> ATTC -> 0112 -> [0 x 4<sup>3</sup> + 1 x 4<sup>2</sup> + 1 x 4<sup>1</sup> + 2 x 4<sup>0</sup>] -> [0 + 16 + 4 + 2] -> 22 </b>

Genes rage in values from AAAA to GGGG or 0 to 255. This gives us an easy way to use genes inside of the car without too much overhead.

#####Genome Specification
The genome is specified in terms of genes as discussed above. This is facilitated with a Genome object that is able to index each gene. We will call these indices *loci*.

The first twelve loci (indices 0 - 11) specify the six coordinates of the the car polygon shape. These are reordered based on the specified CircleSort algorithm.

Following this the wheels are specifief. Loci 12 (assuming starting count at 0) specifies which polygon point to place the first wheel. Loci 13 specifies this wheels radius. Locis 14 and 15 then specify the second wheel.

**The Following Are Examples of Generated Cars**

![car1](Images/car1.png)  
![car2](Images/car2.png)  
![car3](Images/car3.png)

**Planned**

* Modification of car mass
* Modification of wheel mass
* Modification of wheel suspension ratio
* Modification of wheel suspension oscillation frequency
* Modification of wheel physical material
* Modification of wheel power

##### Other Planned Features

* Implementation of coloring of cars to denote lineage
* Implementation of multiple cars at a time
* Implementation of gene recombination
* Implementation of base mutation
* Implementation of roulette selection for gene recombination
* Implementation of stochastic selection for gene recombination
* Implementation of tournament selection for gene recombination
* Imlementation of reward-based selection for gene recombination
* Implementation of elite **n** cars from each generation

##### Licensing
All code is available for reading but may not be reproduced, copied, or used without explicit permission. For permission please contact Dylan Lawrence -- dlawre14@slu.edu
