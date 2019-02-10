import AdvancedComponent from "../core/AdvancedComponent.js";

class Box extends AdvancedComponent {
  constructor(scene) {
    super();

    var geometry = new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshNormalMaterial();

    this.velocity = -0.005;
    this.mesh = new THREE.Mesh(geometry, material);

    scene.add(this.mesh);
  }

  update() {
    if (this.velocity >= 0 && this.mesh.position.x > 2) {
      this.velocity = -0.005;
    }

    if (this.velocity < 0 && this.mesh.position.x < -2) {
      this.velocity = 0.005;
    }

    this.mesh.position.set(
      this.mesh.position.x + this.velocity,
      this.mesh.position.y,
      0
    );
  }
}

export default Box;
