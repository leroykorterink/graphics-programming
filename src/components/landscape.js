import StaticComponent from "../core/StaticComponent.js";

class Landscape extends StaticComponent {
  constructor(scene) {
    super();

    const geometry = new THREE.PlaneGeometry(50, 50, 30);
    const material = new THREE.MeshStandardMaterial({
      color: "#75AA8F",
      side: THREE.DoubleSide,
      roughness: 0.95
    });
    const mesh = new THREE.Mesh(geometry, material);

    mesh.receiveShadow = true;
    mesh.rotation.x = Math.PI / 2;

    scene.add(mesh);
  }
}

export default Landscape;
