## 🧪 Test Results for ${{ inputs.os }}

**Build Status:** ${{ job.status == 'success' && '✅ Passed' || '❌ Failed' }}

### 📊 Test Statistics
| Metric | Value |
|--------|-------|
| **Total Tests** | ${{ env.TEST_TOTAL }} |
| **✅ Passed** | ${{ env.TEST_PASSED }} |
| **❌ Failed** | ${{ env.TEST_FAILED }} |
| **⏭️ Skipped** | ${{ env.TEST_SKIPPED }} |
| **⏱️ Duration** | ${{ env.TEST_DURATION }}s |
| **📈 Coverage** | ${{ env.COVERAGE_PERCENT }} |

### 🔧 Build Information
- **OS:** `${{ inputs.os }}`
- **Configuration:** Release
- **Frameworks:** ${{ startsWith(inputs.os, 'windows') && '.NET & .NET Framework' || '.NET 8.0, 9.0, 10.0' }}
- **Retry Attempts:** Max 3 attempts with 5min timeout

### 📊 Success Rate
${{ env.TEST_TOTAL > 0 && format('**{0:P1}** ({1}/{2} tests passed)', env.TEST_PASSED / env.TEST_TOTAL, env.TEST_PASSED, env.TEST_TOTAL) || 'No tests found' }}

### 📋 Links & Resources
- 📊 [Detailed Test Report](${{ github.server_url }}/${{ github.repository }}/actions/runs/${{ github.run_id }})
- 🔍 [View Test Files](https://github.com/${{ github.repository }}/tree/${{ github.sha }}/src)
- 📈 [Coverage Report](https://codecov.io/${{ github.repository }}/commit/${{ github.sha }})

---
<details>
<summary>🔍 Technical Details</summary>

- **Commit:** [`${{ github.sha }}`](https://github.com/${{ github.repository }}/commit/${{ github.sha }})
- **Branch:** `${{ github.head_ref || github.ref_name }}`
- **Run ID:** `${{ github.run_id }}`
- **Attempt:** `${{ github.run_attempt }}`
- **Workflow:** `${{ github.workflow }}`

</details>

*Last updated: ${{ github.event.head_commit.timestamp }}*