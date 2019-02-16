import Landscape from "../components/Landscape.js";
import Skybox from "../components/Skybox.js";
import Tree from "../components/Tree.js";
import House from "../components/House.js";
import Pine from "../components/Pine.js";
import Lighting from "../components/Lighting.js";
import Mill from "../components/Mill.js";

export default [
  //
  Landscape,
  Lighting,
  Tree,
  Pine(new THREE.Vector3(5, 0, 5)),
  Skybox,
  House(),
  Mill
];
