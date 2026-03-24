using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

public class LightManagement : MonoBehaviour
{
    private int factor = 100000000;
    private NativeArray<float> resultsArray;
    private JobHandle currentJobHandle;

    [BurstCompile(FloatMode = FloatMode.Fast, OptimizeFor = OptimizeFor.Performance)]
    public unsafe struct SqrtBatchJob : IJobParallelForBatch
    {
        [NativeDisableUnsafePtrRestriction]
        public float* resultsPtr;

        public void Execute(int startIndex, int count)
        {
            float* currentPtr = resultsPtr + startIndex;

            for (int i = 0; i < count; i++)
            {
                int actualIndex = startIndex + i;

                *currentPtr = math.sqrt(actualIndex);
                currentPtr++;
            }
        }
    }

    unsafe void Start()
    {
        resultsArray = new NativeArray<float>(factor, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
    }

    unsafe void Update()
    {
        currentJobHandle.Complete();

        float* rawPtr = (float*)NativeArrayUnsafeUtility.GetUnsafePtr(resultsArray);

        SqrtBatchJob job = new SqrtBatchJob
        {
            resultsPtr = rawPtr
        };

        currentJobHandle = job.ScheduleBatch(factor, 16384);
    }

    void LateUpdate()
    {
        currentJobHandle.Complete();
    }

    void OnDestroy()
    {
        currentJobHandle.Complete();
        if (resultsArray.IsCreated)
        {
            resultsArray.Dispose();
        }
    }
}