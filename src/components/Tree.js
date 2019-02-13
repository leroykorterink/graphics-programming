import StaticComponent from "../core/StaticComponent.js";
import Trees from "./Forest.js";

const getRandomClamped = (min = 0.7, max = 1) =>
  Math.random() * (max - min) + min;

class Tree extends StaticComponent {
  constructor(scene) {
    super();

    const leaves = this.createLeaves();
    const trunk = this.createTrunk();

    const tree = new THREE.Group();
    tree.add(trunk);
    tree.add(leaves);

    scene.add(tree);
  }

  createTrunk() {
    const geometry = new THREE.CylinderGeometry(0.25, 0.25, 6, 8, 2, true);
    const material = new THREE.MeshStandardMaterial({
      color: "#382C21",
      roughness: 1,
      metalness: 0.1,
      flatShading: true
    });

    material.flatShading = true;

    const mesh = new THREE.Mesh(geometry, material);

    mesh.position.y += 3;
    mesh.castShadow = true; //default is false
    mesh.receiveShadow = true; //default is false

    return mesh;
  }

  createLeaves() {
    const geometry = new THREE.IcosahedronGeometry(4, 0);
    const material = new THREE.MeshStandardMaterial({
      color: "#008040",
      roughness: 1,
      metalness: 0.1,
      flatShading: true
    });

    var matrix = new THREE.Matrix4();
    matrix.makeScale(
      getRandomClamped(0.6, 0.8),
      getRandomClamped(0.3, 0.6),
      getRandomClamped(0.6, 0.8)
    );

    const mesh = new THREE.Mesh(geometry.applyMatrix(matrix), material);

    mesh.setRotationFromEuler(
      new THREE.Euler(getRandomClamped(0, 1), 0, getRandomClamped(0, 1))
    );

    mesh.position.y += 5;
    mesh.castShadow = true; //default is false

    return mesh;
  }
}

export default Tree;
