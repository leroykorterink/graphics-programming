class Renderer {
  constructor(scene, camera) {
    this.handleCanvasClick = this.handleCanvasClick.bind(this);
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

    this.webGLRenderer.domElement.addEventListener(
      "click",
      this.handleCanvasClick
    );

    document.body.appendChild(this.webGLRenderer.domElement);
  }

  updateSize() {
    this.webGLRenderer.setPixelRatio(window.devicePixelRatio);
    this.webGLRenderer.setSize(window.innerWidth, window.innerHeight);
  }

  handleCanvasClick(clickEvent) {
    this.webGLRenderer.domElement.requestPointerLock();
  }

  render() {
    this.scene.update();
    this.camera.update();

    this.webGLRenderer.render(this.scene.getScene(), this.camera.getCamera());
  }
}

export default Renderer;
