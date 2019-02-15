import StaticComponent from "../core/StaticComponent.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const SIZE = 50;

const loadTexture = makeLoadTexture(fileName => `assets/Landscape/${fileName}`);

class Landscape extends StaticComponent {
  constructor(scene) {
    super();

    this.init(scene);
  }

  async init(scene) {
    const landscapeNormal = await loadTexture("landscape_normal.png");
    landscapeNormal.wrapS = landscapeNormal.wrapT = THREE.RepeatWrapping;
    landscapeNormal.repeat.set(SIZE, SIZE);

    const geometry = new THREE.PlaneGeometry(SIZE, SIZE, 0);
    const material = new THREE.MeshStandardMaterial({
      side: THREE.DoubleSide,
      color: "#193811",
      roughness: 0.95,
      metalness: 0.4,
      normalMap: landscapeNormal
    });

    const mesh = new THREE.Mesh(geometry, material);

    mesh.receiveShadow = true;
    mesh.rotation.x = Math.PI / 2;

    scene.add(mesh);
  }
}

export default Landscape;
