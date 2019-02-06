class Renderer {
  constructor(scene, camera) {
    this.updateSize = this.updateSize.bind(this);

    this.animationFrame = null;

    this.scene = scene;
    this.camera = camera;

    this.webGLRenderer = new THREE.WebGLRenderer({
      antialias: true,
      alpha: true
    });

    window.addEventListener("resize", this.updateSize);
    this.updateSize();

    document.body.appendChild(this.webGLRenderer.domElement);
  }

  updateSize() {
    this.webGLRenderer.setPixelRatio(window.devicePixelRatio);
    this.webGLRenderer.setSize(window.innerWidth, window.innerHeight);
  }

  render() {
    this.scene.update();
    this.camera.update();

    this.webGLRenderer.render(this.scene.getScene(), this.camera.getCamera());
  }
}

export default Renderer;
