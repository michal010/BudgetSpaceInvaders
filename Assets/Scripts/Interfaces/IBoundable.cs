using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoundable {
    public void OnBoundingBoxExit(BoundingBox boundingBox);
}
