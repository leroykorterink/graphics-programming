const createLoadComponent = scene => async componentName => {
  const component = await import(`./components/${componentName}.js`);

  if (typeof component.default === "function") {
    component(scene);
  } else if (component.default && component.default.isObject3D) {
    scene.add(component.default);
  } else {
    throw new Error(`Cannot load component ${componentName}`);
  }

  return component.update;
};

const initialize = async () => {
  const components = await fetch("./components.json", {
    headers: { "Content-Type": "application/json" }
  }).then(res => res.json());

  // Create scene
  const renderer = await import("./core/renderer.js");
  const scene = await import("./core/scene.js");
  const camera = await import("./core/camera.js");

  const loadComponent = createLoadComponent(scene.default);

  const componentUpdateFunctions = Array.from(
    await Promise.all(components.map(loadComponent))
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
