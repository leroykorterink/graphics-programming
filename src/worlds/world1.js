import Skybox from "../components/Skybox.js";
import Tree from "../components/Tree.js";
import House from "../components/House.js";
import Pine from "../components/Pine.js";
import Lighting from "../components/Lighting.js";
import Mill from "../components/Mill.js";
import Island from "../components/Island.js";
import CampFire from "../components/CampFire.js";

export default [
  // General
  Lighting,
  Skybox,

  Island(new THREE.Vector3(0, 1.75, 0)),

  // House
  House(),
  Tree(new THREE.Vector3(-4, 0, -4)),

  // Mill section
  Mill(new THREE.Vector3(12, 0, 10), 35),
  CampFire(new THREE.Vector3(4, 0, 6)),
  Pine(new THREE.Vector3(2, 0, 12)),
  Pine(new THREE.Vector3(18, 0, 3))
];
