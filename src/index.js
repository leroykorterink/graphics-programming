const createLoadComponent = scene => async path => {
  const component = await import(`./components/${path}`);

  if (typeof component.default === "function") {
    component(scene);
  } else if (component.default && component.default.isObject3D) {
    scene.add(component.default);
  } else {
    throw new Error(`Cannot load component ${path}`);
  }

  return component.update;
};

const initialize = async () => {
  // Create scene
  const renderer = await import("./renderer.js");
  const scene = await import("./scene.js");
  const camera = await import("./camera.js");

  const componentPaths = ["box.js"];

  const loadComponent = createLoadComponent(scene.default);
  const componentUpdateFunctions = Array.from(
    await Promise.all(componentPaths.map(loadComponent))
  ).filter(Boolean);

  const render = () => {
    // Request new animation frame before rendering to avoid lag
    requestAnimationFrame(render);

    // Update camera controls
    camera.controls.update();

    // Update components
    componentUpdateFunctions.forEach(f => f());

    // Re-render scene
    renderer.default.render(scene.default, camera.default);
  };

  // Start first render
  render();
};

initialize();
