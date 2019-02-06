import Renderer from "./core/Renderer.js";
import Scene from "./core/Scene.js";
import Camera from "./core/Camera.js";

import world1 from "./worlds/world1.js";

const camera = new Camera();
const scene = new Scene(world1);
const renderer = new Renderer(scene, camera);

const render = () => {
  // Request new animation frame before rendering to avoid lag
  requestAnimationFrame(render);

  // Update camera controls
  scene.update();

  // Re-render scene
  renderer.render();
};

// Start first render
render();
