import AdvancedComponent from "../core/AdvancedComponent.js";

const Smoke = (
  chimneyX = -1,
  chimneyY = 3,
  chimneyZ = -5,
) =>
class extends AdvancedComponent {
  constructor(scene) {
    super();

    this.smokeMaterial = new THREE.MeshBasicMaterial({
      color: 0xffffff,
      transparent: true
    });
    const smoke = new THREE.IcosahedronBufferGeometry(0.2);

    this.smokeMesh = new THREE.Mesh(smoke, this.smokeMaterial);

    this.resetSmoke();

    scene.add(this.smokeMesh);
  }

  update() {
    if(this.smokeMaterial.opacity < 0)
      this.resetSmoke();

    this.smokeMesh.position.set(
      this.smokeMesh.position.x += 0.003,
      this.smokeMesh.position.y += 0.003,
      this.smokeMesh.position.z -= 0.005,
    );

    this.smokeMesh.scale.set(
      this.smokeMesh.scale.x + 0.003,
      this.smokeMesh.scale.y + 0.003,
      this.smokeMesh.scale.z + 0.003,
    );

    this.smokeMaterial.opacity -= 0.001; 
  }

  resetSmoke() {
    this.smokeMesh.position.set(chimneyX,chimneyY,chimneyZ);
    this.smokeMesh.scale.set(1,1,1);
    this.smokeMaterial.opacity = 1; 
  }
}

export default Smoke;
