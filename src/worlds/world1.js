import Landscape from "../components/Landscape.js";
import Skybox from "../components/Skybox.js";
import Tree from "../components/Tree.js";
import Pine from "../components/Pine.js";
import Lighting from "../components/Lighting.js";

export default [
  //
  Landscape,
  Lighting,
  Tree,
  Pine(new THREE.Vector3(5, 0, 5)),
  Skybox
];
