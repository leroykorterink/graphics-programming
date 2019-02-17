import Skybox from "../components/Skybox.js";
import Tree from "../components/Tree.js";
import House from "../components/House.js";
import Pine from "../components/Pine.js";
import Lighting from "../components/Lighting.js";
import Mill from "../components/Mill.js";
import Island from "../components/Island.js";
import CampFire from "../components/CampFire.js";
import Fence from "../components/Fence.js";
import Rock from "../components/Rock.js";
import Rooster from "../components/Rooster.js";
import Airship from "../components/Airship.js";

export default [
  // General
  Lighting,
  Skybox,
  Airship(new THREE.Vector3(0, 10, -50)),

  Island(new THREE.Vector3(0, 1.75, 0)),

  Rock(new THREE.Vector3(16, -0.2, 6)),
  Rock(new THREE.Vector3(15.33, -0.2, 3)),
  Rock(new THREE.Vector3(12.5, -0.2, 5)),

  // House
  House(new THREE.Vector3(4, 0, -13), 0.75),
  Tree(new THREE.Vector3(-14, 0, -6)),
  Tree(new THREE.Vector3(-5, 0, -13)),
  Tree(new THREE.Vector3(-6.3, 0, -3)),
  Tree(new THREE.Vector3(-13, 0, 2)),
  Tree(new THREE.Vector3(15, 0, -11)),

  // Mill section
  Mill(new THREE.Vector3(12, 0, 10), 35),
  Rooster(),
  CampFire(new THREE.Vector3(-1.5, 0, 9)),

  // Forest
  Pine(new THREE.Vector3(1, 0, -1)),
  Pine(new THREE.Vector3(0, 0, -7.5)),
  Pine(new THREE.Vector3(2, 0, 12)),
  Pine(new THREE.Vector3(18, 0, 3)),
  Pine(new THREE.Vector3(4, 0, 18)),
  Pine(new THREE.Vector3(-3, 0, 17)),
  Pine(new THREE.Vector3(-5, 0, 11.5)),
  Pine(new THREE.Vector3(-9, 0, 15)),
  Pine(new THREE.Vector3(-4, 0, 5)),
  Pine(new THREE.Vector3(-9, 0, 8)),
  Pine(new THREE.Vector3(-16, 0, 6.5)),
  Pine(new THREE.Vector3(-14, 0, 11.75)),

  //Fences
  Fence(new THREE.Vector3(6, 0, -17), 0),
  Fence(new THREE.Vector3(9, 0, -13), 0),
];
